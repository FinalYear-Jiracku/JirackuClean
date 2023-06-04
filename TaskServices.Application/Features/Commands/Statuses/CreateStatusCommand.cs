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
    public class CreateStatusCommand : IRequest<Status>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int? SprintId { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class StatusCreatedEvent : BaseEvent
    {
        public Status Status { get; }

        public StatusCreatedEvent(Status status)
        {
            Status = status;
        }
    }
}
