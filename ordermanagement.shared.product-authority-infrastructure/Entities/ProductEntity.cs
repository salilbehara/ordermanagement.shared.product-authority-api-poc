using System;

namespace ordermanagement.shared.product_authority_infrastructure.Entities
{
    public partial class ProductEntity
    {
        public long ProductId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public string ProductKey { get; set; }
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public string PrintIssn { get; set; }
        public string OnlineIssn { get; set; }
        public string ProductTypeCode { get; set; }
        public long PublisherId { get; set; }
        public string PublisherProductCode { get; set; }
        public int LegacyIdSpid { get; set; }
        public string ProductStatusCode { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        public virtual ProductStatusEntity ProductStatus { get; set; }
        public virtual ProductTypeEntity ProductType { get; set; }
    }
}
