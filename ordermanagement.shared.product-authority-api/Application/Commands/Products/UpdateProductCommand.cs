using ordermanagement.shared.product_authority_api.Application.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class UpdateProductCommand : ICommand
    {
        [Required, MaxLength(16)]
        public string ProductKey { get; }

        [Required]
        public DateTime EffectiveStartDate { get; }

        [Required, MaxLength(128)]
        public string ProductName { get; }

        [MaxLength(128)]
        public string ProductDisplayName { get; }

        [MaxLength(8)]
        public string PrintIssn { get; }

        [MaxLength(8)]
        public string OnlineIssn { get; }

        [MaxLength(4)]
        public string ProductTypeCode { get; }

        [Required, MaxLength(4)]
        public string ProductStatusCode { get; }

        [MaxLength(32)]
        public string PublisherProductCode { get; }

        public int LegacyIdSpid { get; }

        public UpdateProductCommand(string productKey, DateTime effectiveStartDate, string productName, string productDisplayName, string printIssn, string onlineIssn,
            string productTypeCode, string productStatusCode, string publisherProductCode, int legacyIdSpid)
        {
            //Add Precondition checks here if required. 
            //This along with property attributes will help avoid transmitting invalid commands to command handlers and enforce the fail fast principle.

            ProductKey = productKey;
            EffectiveStartDate = effectiveStartDate;
            ProductName = productName;
            ProductDisplayName = productDisplayName;
            PrintIssn = printIssn;
            OnlineIssn = onlineIssn;
            ProductTypeCode = productTypeCode;
            ProductStatusCode = productStatusCode;
            PublisherProductCode = publisherProductCode;
            LegacyIdSpid = legacyIdSpid;
        }
    }
}
