//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using ordermanagement.shared.product_authority_api_data_access.Entities;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ordermanagement.shared.product_authority_api_data_access.Configurations
//{
//    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
//    {
//        public void Configure(EntityTypeBuilder<ProductEntity> builder)
//        {
//            //Mapping for table
//            builder.ToTable("products", "dbo");

//            // Set key for entity
//            builder.HasKey(p => new { p.ProductId, p.IsDeleted, p.EffectiveStartDate, p.EffectiveEndDate });

//            //builder.HasMany(x => x.TitleVats).WithOne().HasPrincipalKey(x => x.TitleCode).HasForeignKey(x => x.TitleCode);
//            //builder.HasMany(x => x.TitleSpecialPricings).WithOne().HasForeignKey(x => x.CustomerTitleCatalogId);
//            //builder.HasMany(x => x.TitlePriceRequests).WithOne().HasForeignKey(x => x.CustomerTitleCatalogId);

//            // Set identity for entity (auto increment)
//            //builder.Property(p => p.ProductId).UseSqlServerIdentityColumn();

//            // Set index for entity
//            builder.HasIndex(p => new { p.ProductName, p.IsDeleted, p.EffectiveStartDate, p.EffectiveEndDate });

//            // Set mapping for columns
//            builder.Property(p => p.ProductId).HasColumnType("int").IsRequired().HasColumnName("product_id");
//            //builder.Property(p => p.EffectiveStartDate).HasColumnType("date").IsRequired();
//            //builder.Property(p => p.EbscoAccount).HasColumnType("int").IsRequired();
//            //builder.Property(p => p.TitleCode).HasColumnType("int").IsRequired();
//            //builder.Property(p => p.TitleName).HasColumnType("varchar(120)").IsRequired();
//            //builder.Property(p => p.TitleNote).HasColumnType("varchar(1024)");
//            //builder.Property(p => p.TitleFormatCode).HasColumnType("varchar(2)");
//            //builder.Property(p => p.ISSN).HasColumnType("varchar(8)");
//            //builder.Property(p => p.IssuesPerYear).HasColumnType("smallint");
//            //builder.Property(p => p.PublisherNumber).HasColumnType("int");
//            //builder.Property(p => p.PublisherName).HasColumnType("varchar(40)");
//            //builder.Property(p => p.NewRenewalIndicator).HasColumnType("varchar(1)").IsRequired();
//            //builder.Property(p => p.ListCode).HasColumnType("varchar(3)").IsRequired();
//            //builder.Property(p => p.UnitRetailPrice).HasColumnType("decimal(13,2)").IsRequired();
//            //builder.Property(p => p.BillingISOCurrencyCode).HasColumnType("varchar(3)").IsRequired();
//            //builder.Property(p => p.IsDeleted).HasColumnType("bit").IsRequired().HasDefaultValue(false);
//            //builder.Property(p => p.CreatedDateTime).HasColumnType("datetime").IsRequired().HasDefaultValue(DateTime.UtcNow);
//            //builder.Property(p => p.ModifiedDateTime).HasColumnType("datetime");
//            //builder.Property(p => p.FrequencyCode).HasColumnType("varchar(2)");
//            //builder.Property(p => p.TitleStatus).HasColumnType("varchar(128)");
//            //builder.Property(p => p.ForeignCurrencyCode).HasColumnType("char(2)");
//            //builder.Property(p => p.TranslatedTitleCurrencyCode).HasColumnType("char(2)");
//            //builder.Property(p => p.DispatchCountryCode).HasColumnType("varchar(2)");
//            //builder.Property(p => p.FulfillmentCountryCode).HasColumnType("varchar(3)");
//            //builder.Property(p => p.PublisherCountryCode).HasColumnType("varchar(3)");
//        }

//    }
//}
