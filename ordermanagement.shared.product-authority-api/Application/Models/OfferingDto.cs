namespace ordermanagement.shared.product_authority_api.Application.Queries.Models
{
    public class OfferingDto
    {
        public string OfferingKey { get; set; }
        public string OfferingEdition { get; set; }
        public OfferingFormatDto OfferingFormat { get; set; }
        public OfferingStatusDto OfferingStatus { get; set; }
        public OfferingPlatformDto OfferingPlatform { get; set; }
    }
}
