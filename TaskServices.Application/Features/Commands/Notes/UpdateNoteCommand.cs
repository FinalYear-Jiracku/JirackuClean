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
    public class UpdateNoteCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class NoteUpdatedEvent : BaseEvent
    {
        public Note Note { get; }

        public NoteUpdatedEvent(Note note)
        {
            Note = note;
        }
    }
}
