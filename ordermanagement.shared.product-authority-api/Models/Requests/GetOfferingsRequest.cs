using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Models.Requests
{
    public class GetOfferingsRequest
    {
        [Required]
        public int OfferingId { get; set; }

        public DateTime EffectiveStartDate { get; set; } = DateTime.UtcNow;
    }
}
