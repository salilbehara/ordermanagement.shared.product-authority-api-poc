using FluentMigrator;

namespace ordermanagement.shared.product_authority_database.external.tables
{
    [Migration(MigrationSequence.CreateTableExternalVendors)]
    public class CreateTableExternalVendors : Migration
    {
        public override void Up()
        {
            if (!Schema.Schema("external").Table("vendors").Exists())
            {
                Create.Table("vendors")
                .InSchema("external")
                .WithColumn("vendor_id").AsInt64().NotNullable()
                .WithColumn("effective_start_date").AsDate().NotNullable()
                .WithColumn("effective_end_date").AsDate().NotNullable()
                .WithColumn("vendor_key").AsString(16).NotNullable()
                .WithColumn("vendor_name").AsString(128).NotNullable();

                Create
                    .PrimaryKey("pk_vendors")
                    .OnTable("vendors")
                    .WithSchema("external")
                    .Columns(new string[] { "vendor_id", "effective_start_date" });
            }
        }

        public override void Down()
        {
            if (Schema.Schema("external").Table("vendors").Exists())
            {
                Delete.Table("vendors").InSchema("external");
            }            
        }
    }

}
