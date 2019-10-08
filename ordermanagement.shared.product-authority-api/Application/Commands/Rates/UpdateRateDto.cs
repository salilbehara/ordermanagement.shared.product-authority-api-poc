using System;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Rates
{
    public class UpdateRateDto
    {
        public DateTime? OrderStartDate { get; set; }
        public DateTime? OrderEndDate { get; set; }
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
    }
}
