using FluentMigrator;

namespace ordermanagement.shared.product_authority_database.schemas
{
	[Migration(MigrationSequence.CreateSchemaLookup)]
	public class CreateSchemaLookup : Migration
	{
		public override void Up() => Create.Schema("lookup");
		public override void Down() => Delete.Schema("lookup");
	}	
}