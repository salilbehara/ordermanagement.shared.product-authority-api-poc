namespace ordermanagement.shared.product_authority_api.Application.Queries.Models
{
    public class ProductDto
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
}
