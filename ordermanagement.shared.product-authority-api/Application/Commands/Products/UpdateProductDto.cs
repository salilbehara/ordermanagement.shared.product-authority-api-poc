using ordermanagement.shared.product_authority_api.Interfaces;
using System;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class UpdateProductDto : IProductKey
    {
        public string ProductKey { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public string PrintIssn { get; set; }
        public string OnlineIssn { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductStatusCode { get; set; }
        public string PublisherProductCode { get; set; }
        public int LegacyIdSpid { get; set; }
    }
}
