using System;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Offerings
{
    public class AddOfferingDto
    {
        public DateTime? OrderStartDate { get; set; }
        public string OfferingFormatCode { get; set; }
        public string OfferingPlatformCode { get; set; }
        public string OfferingStatusCode { get; set; }
        public string OfferingEdition { get; set; }
    }
}
