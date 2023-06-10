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
    public class CreateColumnCommand : IRequest<Column>
    {
        public string? Name { get; set; }
        public string? Color { get; set; }
        public int? SprintId { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class ColumnCreatedEvent : BaseEvent
    {
        public Column Column { get; }

        public ColumnCreatedEvent(Column column)
        {
            Column = column;
        }
    }
}
