using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ordermanagement.shared.product_authority_infrastructure.Entities;

namespace ordermanagement.shared.product_authority_infrastructure
{
    public partial class ProductAuthorityDatabaseContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public ProductAuthorityDatabaseContext(DbContextOptions<ProductAuthorityDatabaseContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }
        public virtual DbSet<DeliveryMethodEntity> DeliveryMethods { get; set; }
        public virtual DbSet<OfferingFormatEntity> OfferingFormats { get; set; }
        public virtual DbSet<OfferingPlatformEntity> OfferingPlatforms { get; set; }
        public virtual DbSet<OfferingStatusEntity> OfferingStatuses { get; set; }
        public virtual DbSet<OfferingEntity> Offerings { get; set; }
        public virtual DbSet<ProductStatusEntity> ProductStatuses { get; set; }
        public virtual DbSet<ProductTypeEntity> ProductTypes { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<PublisherEntity> Publishers { get; set; }
        public virtual DbSet<RateClassificationEntity> RateClassifications { get; set; }
        public virtual DbSet<RateTypeEntity> RateTypes { get; set; }
        public virtual DbSet<RateEntity> Rates { get; set; }
        public virtual DbSet<SpidEntity> Spids { get; set; }

        public async Task<bool> SaveChangesAndPublishEventsAsync(IReadOnlyCollection<INotification> domainEvents, CancellationToken cancellationToken = default)
        {
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await base.SaveChangesAsync(cancellationToken);

            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            if (domainEvents != null && domainEvents.Count > 0)
            {
                await _mediator.DispatchDomainEventsAsync(this, domainEvents);
            }

            return true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You did not see this. Just imagine it is masked.
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=product-authority.cluster-c4w82moezzdt.us-east-1.rds.amazonaws.com;Database=product_authority_3;Username=test;Password=test1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("uuid-ossp")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<DeliveryMethodEntity>(entity =>
            {
                entity.HasKey(e => e.DeliveryMethodCode)
                    .HasName("pk_delivery_methods");

                entity.ToTable("delivery_methods", "lookup");

                entity.Property(e => e.DeliveryMethodCode)
                    .HasColumnName("delivery_method_code")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeliveryMethodName)
                    .IsRequired()
                    .HasColumnName("delivery_method_name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<OfferingFormatEntity>(entity =>
            {
                entity.HasKey(e => e.OfferingFormatCode)
                    .HasName("pk_offering_formats");

                entity.ToTable("offering_formats", "lookup");

                entity.Property(e => e.OfferingFormatCode)
                    .HasColumnName("offering_format_code")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();

                entity.Property(e => e.OfferingFormatName)
                    .IsRequired()
                    .HasColumnName("offering_format_name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<OfferingPlatformEntity>(entity =>
            {
                entity.HasKey(e => e.OfferingPlatformCode)
                    .HasName("pk_offering_platforms");

                entity.ToTable("offering_platforms", "lookup");

                entity.Property(e => e.OfferingPlatformCode)
                    .HasColumnName("offering_platform_code")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();

                entity.Property(e => e.OfferingPlatformName)
                    .IsRequired()
                    .HasColumnName("offering_platform_name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<OfferingStatusEntity>(entity =>
            {
                entity.HasKey(e => e.OfferingStatusCode)
                    .HasName("pk_offering_statuses");

                entity.ToTable("offering_statuses", "lookup");

                entity.Property(e => e.OfferingStatusCode)
                    .HasColumnName("offering_status_code")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();

                entity.Property(e => e.OfferingStatusName)
                    .IsRequired()
                    .HasColumnName("offering_status_name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<OfferingEntity>(entity =>
            {
                entity.HasKey(e => new { e.OfferingId, e.EffectiveStartDate })
                    .HasName("pk_offerings");

                entity.ToTable("offerings", "dbo");

                entity.HasIndex(e => new { e.OfferingKey, e.EffectiveEndDate })
                    .HasName("idx_offerings_offering_key_effective_end_date");

                entity.HasIndex(e => new { e.ProductId, e.EffectiveEndDate })
                    .HasName("idx_offerings_product_id_effective_end_date");

                entity.Property(e => e.OfferingId)
                    .HasColumnName("offering_id")
                    .ValueGeneratedOnAdd()
                    .UseNpgsqlIdentityByDefaultColumn();

                entity.Property(e => e.OfferingKey)
                    .IsRequired()
                    .HasColumnName("offering_key")
                    .HasMaxLength(16);

                entity.Property(e => e.EffectiveStartDate)
                    .HasColumnName("effective_start_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.EffectiveEndDate)
                    .HasColumnName("effective_end_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'9999-12-31'::date");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasColumnName("added_by")
                    .HasMaxLength(64);

                entity.Property(e => e.AddedOnUtc)
                    .HasColumnName("added_on_utc")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.OfferingEdition)
                    .HasColumnName("offering_edition")
                    .HasMaxLength(64);

                entity.Property(e => e.OfferingFormatCode)
                    .IsRequired()
                    .HasColumnName("offering_format_code")
                    .HasMaxLength(4);

                entity.Property(e => e.OfferingPlatformCode)
                    .HasColumnName("offering_platform_code")
                    .HasMaxLength(4);

                entity.Property(e => e.OfferingStatusCode)
                    .IsRequired()
                    .HasColumnName("offering_status_code")
                    .HasMaxLength(4);

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedOnUtc)
                    .HasColumnName("updated_on_utc")
                    .HasColumnType("date");

                entity.HasOne(d => d.OfferingFormat)
                    .WithMany(p => p.Offerings)
                    .HasForeignKey(d => d.OfferingFormatCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_offerings_offering_formats");

                entity.HasOne(d => d.OfferingPlatform)
                    .WithMany(p => p.Offerings)
                    .HasForeignKey(d => d.OfferingPlatformCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_offerings_offering_platforms");

                entity.HasOne(d => d.OfferingStatus)
                    .WithMany(p => p.Offerings)
                    .HasForeignKey(d => d.OfferingStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_offerings_offering_statuses");
            });

            modelBuilder.Entity<ProductStatusEntity>(entity =>
            {
                entity.HasKey(e => e.ProductStatusCode)
                    .HasName("pk_product_statuses");

                entity.ToTable("product_statuses", "lookup");

                entity.Property(e => e.ProductStatusCode)
                    .HasColumnName("product_status_code")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductStatusName)
                    .IsRequired()
                    .HasColumnName("product_status_name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<ProductTypeEntity>(entity =>
            {
                entity.HasKey(e => e.ProductTypeCode)
                    .HasName("pk_product_types");

                entity.ToTable("product_types", "lookup");

                entity.Property(e => e.ProductTypeCode)
                    .HasColumnName("product_type_code")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductTypeName)
                    .IsRequired()
                    .HasColumnName("product_type_name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.EffectiveStartDate })
                    .HasName("pk_products");

                entity.ToTable("products", "dbo");

                entity.HasIndex(e => new { e.ProductKey, e.EffectiveStartDate, e.EffectiveEndDate })
                    .HasName("idx_products_product_key_with_date_range");

                entity.HasIndex(e => new { e.ProductName, e.EffectiveStartDate, e.EffectiveEndDate })
                    .HasName("idx_products_product_name_with_date_range");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .ValueGeneratedOnAdd()
                    .UseNpgsqlIdentityByDefaultColumn();

                entity.Property(e => e.EffectiveStartDate)
                    .HasColumnName("effective_start_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.EffectiveEndDate)
                    .HasColumnName("effective_end_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'9999-12-31'::date");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasColumnName("added_by")
                    .HasMaxLength(64);

                entity.Property(e => e.AddedOnUtc)
                    .HasColumnName("added_on_utc")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.LegacyIdSpid).HasColumnName("legacy_id_spid");

                entity.Property(e => e.OnlineIssn)
                    .HasColumnName("online_issn")
                    .HasMaxLength(8);

                entity.Property(e => e.PrintIssn)
                    .HasColumnName("print_issn")
                    .HasMaxLength(8);

                entity.Property(e => e.ProductDisplayName)
                    .HasColumnName("product_display_name")
                    .HasMaxLength(128);

                entity.Property(e => e.ProductKey)
                    .IsRequired()
                    .HasColumnName("product_key")
                    .HasMaxLength(16);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasMaxLength(128);

                entity.Property(e => e.ProductStatusCode)
                    .IsRequired()
                    .HasColumnName("product_status_code")
                    .HasMaxLength(4);

                entity.Property(e => e.ProductTypeCode)
                    .HasColumnName("product_type_code")
                    .HasMaxLength(4);

                entity.Property(e => e.PublisherId).HasColumnName("publisher_id");

                entity.Property(e => e.PublisherProductCode)
                    .HasColumnName("publisher_product_code")
                    .HasMaxLength(32);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedOnUtc)
                    .HasColumnName("updated_on_utc")
                    .HasColumnType("date");

                entity.HasOne(d => d.ProductStatus)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductStatusCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_product_statuses");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_product_types");
            });

            modelBuilder.Entity<PublisherEntity>(entity =>
            {
                entity.HasKey(e => new { e.PublisherId, e.EffectiveStartDate })
                    .HasName("pk_publishers");

                entity.ToTable("publishers", "dbo");

                entity.Property(e => e.PublisherId)
                    .HasColumnName("publisher_id")
                    .ValueGeneratedOnAdd()
                    .UseNpgsqlIdentityByDefaultColumn();

                entity.Property(e => e.EffectiveStartDate)
                    .HasColumnName("effective_start_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.EffectiveEndDate)
                    .HasColumnName("effective_end_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'9999-12-31'::date");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasColumnName("added_by")
                    .HasMaxLength(64);

                entity.Property(e => e.AddedOnUtc)
                    .HasColumnName("added_on_utc")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.PublisherKey)
                    .IsRequired()
                    .HasColumnName("publisher_key")
                    .HasMaxLength(16);

                entity.Property(e => e.PublisherName)
                    .IsRequired()
                    .HasColumnName("publisher_name")
                    .HasMaxLength(128);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedOnUtc)
                    .HasColumnName("updated_on_utc")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<RateClassificationEntity>(entity =>
            {
                entity.HasKey(e => new { e.RateClassificationId, e.EffectiveStartDate })
                    .HasName("pk_rate_classifications");

                entity.ToTable("rate_classifications", "dbo");

                entity.Property(e => e.RateClassificationId)
                    .HasColumnName("rate_classification_id")
                    .ValueGeneratedOnAdd()
                    .UseNpgsqlIdentityByDefaultColumn();

                entity.Property(e => e.EffectiveStartDate)
                    .HasColumnName("effective_start_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.EffectiveEndDate)
                    .HasColumnName("effective_end_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'9999-12-31'::date");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasColumnName("added_by")
                    .HasMaxLength(64);

                entity.Property(e => e.AddedOnUtc)
                    .HasColumnName("added_on_utc")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.FteTierId).HasColumnName("fte_tier_id");

                entity.Property(e => e.GeoGroupId).HasColumnName("geo_group_id");

                entity.Property(e => e.PublisherId).HasColumnName("publisher_id");

                entity.Property(e => e.RateClassificationKey)
                    .IsRequired()
                    .HasColumnName("rate_classification_key")
                    .HasMaxLength(16);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedOnUtc)
                    .HasColumnName("updated_on_utc")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<RateTypeEntity>(entity =>
            {
                entity.HasKey(e => e.RateTypeCode)
                    .HasName("pk_rate_types");

                entity.ToTable("rate_types", "lookup");

                entity.Property(e => e.RateTypeCode)
                    .HasColumnName("rate_type_code")
                    .HasMaxLength(4)
                    .ValueGeneratedNever();

                entity.Property(e => e.RateTypeName)
                    .IsRequired()
                    .HasColumnName("rate_type_name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<RateEntity>(entity =>
            {
                entity.HasKey(e => new { e.RateId, e.EffectiveStartDate })
                    .HasName("pk_rates");

                entity.ToTable("rates", "dbo");

                entity.HasIndex(e => new { e.OfferingId, e.EffectiveEndDate })
                    .HasName("idx_rates_offering_id_effective_end_date");

                entity.HasIndex(e => new { e.ProductId, e.EffectiveEndDate })
                    .HasName("idx_rates_product_id_effective_end_date");

                entity.HasIndex(e => new { e.RateKey, e.EffectiveEndDate })
                    .HasName("idx_rates_rate_key_effective_end_date");

                entity.Property(e => e.RateId)
                    .HasColumnName("rate_id")
                    .ValueGeneratedOnAdd()
                    .UseNpgsqlIdentityByDefaultColumn();

                entity.Property(e => e.EffectiveStartDate)
                    .HasColumnName("effective_start_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.EffectiveEndDate)
                    .HasColumnName("effective_end_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'9999-12-31'::date");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasColumnName("added_by")
                    .HasMaxLength(64);

                entity.Property(e => e.AddedOnUtc)
                    .HasColumnName("added_on_utc")
                    .HasColumnType("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.CommissionAmount)
                    .HasColumnName("commission_amount")
                    .HasColumnType("numeric(9,2)");

                entity.Property(e => e.CommissionPercent)
                    .HasColumnName("commission_percent")
                    .HasColumnType("numeric(5,2)");

                entity.Property(e => e.CostAmount)
                    .HasColumnName("cost_amount")
                    .HasColumnType("numeric(9,2)");

                entity.Property(e => e.DeliveryMethodCode)
                    .IsRequired()
                    .HasColumnName("delivery_method_code")
                    .HasMaxLength(4);

                entity.Property(e => e.EffortKey)
                    .HasColumnName("effort_key")
                    .HasMaxLength(64);

                entity.Property(e => e.LegacyIdTitleNumber).HasColumnName("legacy_id_title_number");

                entity.Property(e => e.ListCode)
                    .HasColumnName("list_code")
                    .HasMaxLength(4);

                entity.Property(e => e.NewRenewalRateIndicator)
                    .IsRequired()
                    .HasColumnName("new_renewal_rate_indicator")
                    .HasMaxLength(1);

                entity.Property(e => e.OfferingId).HasColumnName("offering_id");

                entity.Property(e => e.PostageAmount)
                    .HasColumnName("postage_amount")
                    .HasColumnType("numeric(7,2)");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.RateClassificationId).HasColumnName("rate_classification_id");

                entity.Property(e => e.RateKey)
                    .IsRequired()
                    .HasColumnName("rate_key")
                    .HasMaxLength(16);

                entity.Property(e => e.RateTypeCode)
                    .HasColumnName("rate_type_code")
                    .HasMaxLength(4);

                entity.Property(e => e.TermLength).HasColumnName("term_length");

                entity.Property(e => e.TermUnit)
                    .IsRequired()
                    .HasColumnName("term_unit")
                    .HasMaxLength(4);

                entity.Property(e => e.UnitRetailAmount)
                    .HasColumnName("unit_retail_amount")
                    .HasColumnType("numeric(9,2)");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedOnUtc)
                    .HasColumnName("updated_on_utc")
                    .HasColumnType("date");

                entity.HasOne(d => d.DeliveryMethod)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.DeliveryMethodCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rates_delivery_methods");

                entity.HasOne(d => d.RateType)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.RateTypeCode)
                    .HasConstraintName("fk_rates_rate_type_codes");
            });

            modelBuilder.Entity<SpidEntity>(entity =>
            {
                entity.HasKey(e => e.Spid)
                    .HasName("pk_spid");

                entity.ToTable("spids", "lookup");

                entity.Property(e => e.Spid)
                    .HasColumnName("spid");
            });
        }
    }
}