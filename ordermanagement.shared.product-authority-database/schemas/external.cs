using FluentMigrator;

namespace ordermanagement.shared.product_authority_database.schemas
{
	[Migration(MigrationSequence.CreateSchemaExternal)]
	public class CreateSchemaExternal : Migration
	{
		public override void Up() => Create.Schema("external");
		public override void Down() => Delete.Schema("external");
	}	
}