namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class AddProductDto
    {
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public long PublisherId { get; set; }
        public string PrintIssn { get; set; }
        public string OnlineIssn { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductStatusCode { get; set; }
        public string PublisherProductCode { get; set; }
        public int LegacyIdSpid { get; set; }
    }
}
