using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Models.Requests
{
    public class AddProductRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime EffectiveStartDate { get; set; }

        [Required]
        public DateTime EffectiveEndDate { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string OnlineIssn { get; set; }

        public string PrintIssn { get; set; }

        [Required]
        public string ProductStatusCode { get; set; }

        [Required]
        public string ProductTypeCode { get; set; }

        [Required]
        public int PublisherId { get; set; }

        public string PublisherProductCode { get; set; }

        public int LegacyIdSpid { get; set; }
    }
}
