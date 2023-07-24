using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common.Interfaces;

namespace TaskServices.Domain.Common
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly IIssueEventPublisher _issueEventPublisher;

        public DomainEventDispatcher(IMediator mediator, IIssueEventPublisher issueEventPublisher)
        {
            _mediator = mediator;
            _issueEventPublisher = issueEventPublisher;
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
                    _issueEventPublisher.SendMessage(domainEvent);
                }
            }
        }
    }
}
