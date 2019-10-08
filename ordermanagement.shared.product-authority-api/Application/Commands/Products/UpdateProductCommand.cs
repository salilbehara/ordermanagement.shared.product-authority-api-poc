using MediatR;
using ordermanagement.shared.product_authority_api.Application.Events;
using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanagement.shared.product_authority_api.Application.Commands.Products
{
    public class UpdateProductCommand : CommandEvent, IRequest
    {
        public string ProductKey { get; }
        public DateTime EffectiveStartDate { get; }
        public string ProductName { get; }
        public string ProductDisplayName { get; }
        public string PrintIssn { get; }
        public string OnlineIssn { get; }
        public string ProductTypeCode { get; }
        public string ProductStatusCode { get; }
        public string PublisherProductCode { get; }
        public int LegacyIdSpid { get; }

        public UpdateProductCommand(string productKey, DateTime effectiveStartDate, string productName, string productDisplayName, string printIssn, string onlineIssn,
            string productTypeCode, string productStatusCode, string publisherProductCode, int legacyIdSpid)
        {
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

            //Use Mediator to publish the messages --> https://github.com/jbogard/MediatR/wiki
            var productChangedPublishToSnsTopicEvent = new ProductChangedEvent(productName, productDisplayName, printIssn, onlineIssn,
               publisherProductCode, "product-updated");

            AddCommandEvent(productChangedPublishToSnsTopicEvent);

        }
    }
}
