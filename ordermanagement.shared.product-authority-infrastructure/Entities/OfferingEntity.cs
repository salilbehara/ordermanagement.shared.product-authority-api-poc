using System;

namespace ordermanagement.shared.product_authority_infrastructure.Entities
{
    public partial class OfferingEntity
    {
        public long OfferingId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public string OfferingKey { get; set; }
        public long ProductId { get; set; }
        public string OfferingFormatCode { get; set; }
        public string OfferingPlatformCode { get; set; }
        public string OfferingStatusCode { get; set; }
        public string OfferingEdition { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        public virtual OfferingFormatEntity OfferingFormat { get; set; }
        public virtual OfferingStatusEntity OfferingStatus { get; set; }
        public virtual OfferingPlatformEntity OfferingPlatform { get; set; }
    }
}
