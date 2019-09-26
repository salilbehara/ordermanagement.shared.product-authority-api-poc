using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Models
{
    public class Rate
    {
        [Required]
        public string RateKey { get; set; }

        [Required]
        public DateTime EffectiveStartDate { get; set; }

        [Required]
        public DateTime EffectiveEndDate { get; set; }

        [Required]
        public long OfferingKey { get; set; }

        [Required]
        public long ProductKey { get; set; }

        [Required]
        public long RateClassificationId { get; set; }

        public decimal? UnitRetailAmount { get; set; }

        public decimal? CommissionAmount { get; set; }

        public decimal? CommissionPercent { get; set; }

        public decimal? CostAmount { get; set; }

        public decimal? PostageAmount { get; set; }

        [Required]
        public string DeliveryMethodCode { get; set; }

        [Required]
        public int TermLength { get; set; }

        [Required]
        public string TermUnit { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string NewRenewalRateIndicator { get; set; }

        public string EffortKey { get; set; }

        public int? LegacyIdTitleNumber { get; set; }

        public string ListCode { get; set; }

        public string RateTypeCode { get; set; }
    }
}
