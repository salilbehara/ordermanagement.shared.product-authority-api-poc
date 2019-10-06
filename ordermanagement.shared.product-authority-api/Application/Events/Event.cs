using System;

namespace ordermanagement.shared.product_authority_api.Application.Events
{
    public abstract class Event
    {
        public string EventName { get; set; }
        public Guid EventId { get; set; }
        public Guid CorrelationId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
