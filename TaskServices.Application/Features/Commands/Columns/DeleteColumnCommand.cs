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
    public class DeleteColumnCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteColumnCommand(int id)
        {
            Id = id;
        }
    }
    public class ColumnDeletedEvent : BaseEvent
    {
        public Column Column { get; }

        public ColumnDeletedEvent(Column column)
        {
            Column = column;
        }
    }
}
