using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Models.Requests
{
    public class GetProductRequest
    {
        public int? ProductId { get; set; }

        [StringLength(16)]
        public string ProductKey { get; set; }

        public DateTime EffectiveStartDate { get; set; } = DateTime.UtcNow;

        public bool IncludeOfferingsAndRates { get; set; } = true;
    }
}
