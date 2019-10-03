namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    //Product ViewModels / DTOs(Data Transfer Objects) will be returned from the server-side to client apps.
    //This ViewModels hold the data the way the client app needs.

    public class GetProductBasedOnEffectiveStartDateQueryDto
    {
        public string ProductKey { get; set; }
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public string PrintIssn { get; set; }
        public string OnlineIssn { get; set; }
        public string PublisherProductCode { get; set; }
        public int LegacyIdSpid { get; set; }
        public long PublisherId { get; set; }
        public ProductStatusDto ProductStatus { get; set; }
        public ProductTypeDto ProductType { get; set; }
    }

    public class ProductStatusDto
    {
        public string ProductStatusCode { get; set; }
        public string ProductStatusName { get; set; }
    }

    public class ProductTypeDto
    {
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
    }
}