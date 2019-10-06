using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class AddProductCommand : IRequest
    {
        [Required, MaxLength(128)]
        public string ProductName { get; }

        [MaxLength(128)]
        public string ProductDisplayName { get; }

        [Required, Range(1, long.MaxValue, ErrorMessage = "The field PublisherId must be greater than 0")]
        public long PublisherId { get; }

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

        public AddProductCommand(string productName, string productDisplayName, long publisherId, string printIssn, string onlineIssn, string productTypeCode,
            string productStatusCode, string publisherProductCode, int legacyIdSpid)
        {
            //Add Precondition checks here if required. 
            //This along with property attributes will help avoid transmitting invalid commands to command handlers and enforce the fail fast principle.

            ProductName = productName;
            ProductDisplayName = productDisplayName;
            PublisherId = publisherId;
            PrintIssn = printIssn;
            OnlineIssn = onlineIssn;
            ProductTypeCode = productTypeCode;
            ProductStatusCode = productStatusCode;
            PublisherProductCode = publisherProductCode;
            LegacyIdSpid = legacyIdSpid;
        }
    }
}
