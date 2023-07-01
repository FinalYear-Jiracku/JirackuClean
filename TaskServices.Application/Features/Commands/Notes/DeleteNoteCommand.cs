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
    public class DeleteNoteCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteNoteCommand(int id)
        {
            Id = id;
        }
    }
    public class NoteDeletedEvent : BaseEvent
    {
        public Note Note { get; }

        public NoteDeletedEvent(Note note)
        {
            Note = note;
        }
    }
}
