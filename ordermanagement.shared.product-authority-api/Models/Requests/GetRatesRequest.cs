using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Models.Requests
{
    public class GetRatesRequest
    {
        [Required]
        public int RateId { get; set; }

        public DateTime EffectiveStartDate { get; set; } = DateTime.UtcNow;
    }
}
