namespace ordermanagement.shared.product_authority_api.Application.Models
{
    public class RateDto
    {
        public string RateKey { get; set; }
        public decimal? UnitRetailAmount { get; set; }
        public decimal? CommissionAmount { get; set; }
        public decimal? CommissionPercent { get; set; }
        public decimal? CostAmount { get; set; }
        public decimal? PostageAmount { get; set; }
        public int TermLength { get; set; }
        public string TermUnit { get; set; }
        public int Quantity { get; set; }
        public string NewRenewalRateIndicator { get; set; }
        public string EffortKey { get; set; }
        public int? LegacyIdTitleNumber { get; set; }
        public string ListCode { get; set; }
        public DeliveryMethodDto DeliveryMethod { get; set; }
        public RateTypeDto RateType { get; set; }
    }
}
