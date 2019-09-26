using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Models
{
    public class Product
    {
        [Required]
        public string ProductKey { get; set; }
        [Required]
        public DateTime EffectiveStartDate { get; set; }
        [Required]
        public DateTime EffectiveEndDate { get; set; }
        [Required]
        public string OnlineIssn { get; set; }
        public string PrintIssn { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductStatusCode { get; set; }
        [Required]
        public string ProductTypeCode { get; set; }
        [Required]
        public long PublisherId { get; set; }
        public string PublisherProductCode { get; set; }
        public int LegacyIdSpid { get; set; }
    }
}
