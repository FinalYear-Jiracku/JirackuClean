using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Columns
{
    public class UpdateColumnCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class ColumnUpdatedEvent : BaseEvent
    {
        public Column Column { get; }

        public ColumnUpdatedEvent(Column column)
        {
            Column = column;
        }
    }
}
