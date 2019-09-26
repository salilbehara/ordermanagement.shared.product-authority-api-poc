using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Models
{
    public class Offering
    {
        [Required]
        public string OfferingKey { get; set; }
        [Required]
        public DateTime EffectiveStartDate { get; set; }
        [Required]
        public DateTime EffectiveEndDate { get; set; }
        [Required]
        public string OfferingStatus { get; set; }
        [Required]
        public string OfferingFormat { get; set; }
    }
}
