using System;
using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api_data_access.Entities
{
    public partial class RateEntity
    {
        public long RateId { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public string RateKey { get; set; }
        public long OfferingId { get; set; }
        public long ProductId { get; set; }
        public long RateClassificationId { get; set; }
        public decimal? UnitRetailAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public decimal? CommissionPercent { get; set; }
        public decimal? CostAmount { get; set; }
        public decimal? PostageAmount { get; set; }
        public string DeliveryMethodCode { get; set; }
        public int TermLength { get; set; }
        public string TermUnit { get; set; }
        public int Quantity { get; set; }
        public string NewRenewalRateIndicator { get; set; }
        public string EffortKey { get; set; }
        public int? LegacyIdTitleNumber { get; set; }
        public string ListCode { get; set; }
        public string RateTypeCode { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOnUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        public virtual DeliveryMethodEntity DeliveryMethod { get; set; }
        public virtual RateTypeEntity RateType { get; set; }
    }
}
