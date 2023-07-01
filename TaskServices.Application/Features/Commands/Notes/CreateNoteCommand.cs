using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Common;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Commands.Notes
{
    public class CreateNoteCommand : IRequest<Note>
    {
        public string? Content { get; set; }
        public int? Order { get; set; }
        public int? UserId { get; set; }
        public int? ColumnId { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class NoteCreatedEvent : BaseEvent
    {
        public Note Note { get; }

        public NoteCreatedEvent(Note note)
        {
            Note = note;
        }
    }
}
