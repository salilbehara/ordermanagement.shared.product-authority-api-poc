using MediatR;
using System;

namespace ordermanagement.shared.product_authority_api.Application.Events
{
    public class ProductChangedEvent : Event, INotification
    {
        public string ProductName { get; private set; }
        public string ProductDisplayName { get; private set; }
        public string PrintIssn { get; private set; }
        public string OnlineIssn { get; private set; }
        public string PublisherProductCode { get; private set; }

        public ProductChangedEvent(string productName, string productDisplayName, string printIssn, string onlineIssn, 
            string publisherProductCode, string eventName)
        {
            ProductName = productName;
            ProductDisplayName = productDisplayName;
            PrintIssn = printIssn;
            OnlineIssn = onlineIssn;
            PublisherProductCode = publisherProductCode;
            EventName = eventName;
            EventId = Guid.NewGuid();
            CorrelationId = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
        }
    }
}
