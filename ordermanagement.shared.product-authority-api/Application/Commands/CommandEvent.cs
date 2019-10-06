using MediatR;
using System.Collections.Generic;

namespace ordermanagement.shared.product_authority_api.Application.Commands
{
    public abstract class CommandEvent
    {
        private List<INotification> _commandEvents;
        public IReadOnlyCollection<INotification> CommandEvents => _commandEvents?.AsReadOnly();

        public void AddCommandEvent(INotification eventItem)
        {
            _commandEvents = _commandEvents ?? new List<INotification>();
            _commandEvents.Add(eventItem);
        }

        public void RemoveCommandEvent(INotification eventItem)
        {
            _commandEvents?.Remove(eventItem);
        }

        public void ClearCommandEvents()
        {
            _commandEvents?.Clear();
        }
    }
}
