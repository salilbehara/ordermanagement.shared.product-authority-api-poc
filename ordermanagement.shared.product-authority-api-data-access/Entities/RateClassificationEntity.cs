using System;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class RateClassificationEntity
    {
        public long RateClassificationId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public string RateClassificationKey { get; set; }
        public long PublisherId { get; set; }
        public int GeoGroupId { get; set; }
        public int FteTierId { get; set; }
        public int CategoryId { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
