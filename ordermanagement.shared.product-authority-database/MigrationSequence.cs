namespace ordermanagement.shared.product_authority_database
{
	public static class MigrationSequence
	{
		/* Schemas */
		public const long CreateSchemaApp = 1;
        public const long CreateSchemaLookup = 2;
        public const long CreateSchemaExternal = 3;

        /* External - Vendor */
        public const long CreateTableExternalVendors = 4;
    }
}