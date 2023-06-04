using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Statuses
{
    public class DeleteStatusCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteStatusCommand(int id)
        {
            Id = id;
        }
    }
    public class StatusDeletedEvent : BaseEvent
    {
        public Status Status { get; }

        public StatusDeletedEvent(Status status)
        {
            Status = status;
        }
    }
}
