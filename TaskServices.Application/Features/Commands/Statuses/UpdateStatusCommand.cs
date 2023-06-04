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
    public class UpdateStatusCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
        public UpdateStatusCommand(int id, string? name, string? color, string? updatedBy, DateTimeOffset? updatedAt)
        {
            Id = id;
            Name = name;
            Color = color;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
    public class StatusUpdatedEvent : BaseEvent
    {
        public Status Status { get; }

        public StatusUpdatedEvent(Status status)
        {
            Status = status;
        }
    }
}
