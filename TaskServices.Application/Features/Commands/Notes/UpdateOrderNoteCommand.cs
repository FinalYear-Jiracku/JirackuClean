using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskServices.Application.Features.Commands.Notes
{
    public class UpdateOrderNoteCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public int Order { get; set; }
        public UpdateOrderNoteCommand(int id, int columnId, int order)
        {
            Id = id;
            ColumnId = columnId;
            Order = order;
        }
    }
}
