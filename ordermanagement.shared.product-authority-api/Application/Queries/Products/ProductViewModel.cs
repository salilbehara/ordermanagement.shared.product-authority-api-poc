using System;

namespace ordermanagement.shared.product_authority_api.Application.Queries.Products
{
    //Product ViewModels / DTOs(Data Transfer Objects) will be returned from the server-side to client apps.
    //This ViewModels hold the data the way the client app needs.

    public class Product
    {
        public string ProductKey { get; set; }
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public string PrintIssn { get; set; }
        public string OnlineIssn { get; set; }
        public string PublisherProductCode { get; set; }
        public int LegacyIdSpid { get; set; }
        public long PublisherId { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ProductType ProductType { get; set; }
    }

    public class ProductStatus
    {
        public string ProductStatusCode { get; set; }
        public string ProductStatusName { get; set; }
    }

    public class ProductType
    {
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
    }
}