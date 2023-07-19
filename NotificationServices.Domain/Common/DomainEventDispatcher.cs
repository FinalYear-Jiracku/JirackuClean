using MediatR;
using NotificationServices.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Domain.Common
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly INotificationEventPulisher _notificationEventPulisher;

        public DomainEventDispatcher(IMediator mediator, INotificationEventPulisher notificationEventPulisher)
        {
            _mediator = mediator;
            _notificationEventPulisher = notificationEventPulisher;
        }

        public async Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents)
        {
            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.DomainEvents.ToArray();

                entity.ClearDomainEvents();

                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent).ConfigureAwait(false);
                    //_notificationEventPulisher.SendMessage(domainEvent);
                }
            }
        }
    }
}
