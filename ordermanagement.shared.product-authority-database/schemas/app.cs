using FluentMigrator;

namespace ordermanagement.shared.product_authority_database.schemas
{
	[Migration(MigrationSequence.CreateSchemaApp)]
	public class CreateSchemaApp : Migration
	{
		public override void Up() => Create.Schema("app");
		public override void Down() => Delete.Schema("app");
	}	
}