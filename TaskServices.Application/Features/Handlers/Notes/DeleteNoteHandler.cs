using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Notes;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Notes
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
        {
            var note = await _unitOfWork.NoteRepository.GetNoteById(command.Id);
            if (note == null)
            {
                return default;
            }
            note.IsDeleted = true;
            await _unitOfWork.Repository<Note>().UpdateAsync(note);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
