using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Domain.Common.Interfaces;

namespace UserServices.Domain.Common
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly IUserEventPublisher _userEventPublisher;

        public DomainEventDispatcher(IMediator mediator, IUserEventPublisher userEventPublisher)
        {
            _mediator = mediator;
            _userEventPublisher = userEventPublisher;
        }

        public async Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents)
        {
            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.DomainEvents.ToArray();

                entity.ClearDomainEvents();

                foreach (var domainEvent in events)
                {
                    //await _mediator.Publish(domainEvent).ConfigureAwait(false);
                    _userEventPublisher.SendMessage(domainEvent);
                }
            }
        }
    }
}
