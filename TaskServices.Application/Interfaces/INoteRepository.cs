using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Commands.Notes;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface INoteRepository
    {
        int CheckOrder(int? columnId);
        Task<Note> GetNoteById(int id);
        Task<List<Note>> UpdateOrderNote(UpdateOrderNoteCommand command);
    }
}
