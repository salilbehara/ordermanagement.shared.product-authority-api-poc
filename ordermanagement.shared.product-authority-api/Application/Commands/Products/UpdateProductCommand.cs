using ordermanagement.shared.product_authority_api.Application.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class UpdateProductCommand : ICommand
    {
        [Required]
        public string ProductKey { get; set; }

        [Required]
        public DateTime EffectiveStartDate { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string ProductDisplayName { get; set; }

        public string PrintIssn { get; set; }

        public string OnlineIssn { get; set; }

        public string ProductTypeCode { get; set; }

        [Required]
        public string ProductStatusCode { get; set; }

        public string PublisherProductCode { get; set; }

        public int LegacyIdSpid { get; set; }
    }
}
