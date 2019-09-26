using System;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class PublisherEntity
    {
        public long PublisherId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public string PublisherKey { get; set; }
        public string PublisherName { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
