using MediatR;
using ordermanagement.shared.product_authority_api.Application.Events;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class AddProductCommand : CommandEvent, IRequest
    {
        public string ProductName { get; }
        public string ProductDisplayName { get; }
        public long PublisherId { get; }
        public string PrintIssn { get; }
        public string OnlineIssn { get; }
        public string ProductTypeCode { get; }
        public string ProductStatusCode { get; }
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

            var productChangedPublishToSnsTopicEvent = new ProductChangedEvent(productName, productDisplayName, printIssn, onlineIssn, 
                publisherProductCode, "product-added");

            AddCommandEvent(productChangedPublishToSnsTopicEvent);
        }
    }
}
