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
    public class UpdateNoteHandler : IRequestHandler<UpdateNoteCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            var note = await _unitOfWork.NoteRepository.GetNoteById(command.Id);
            if (note == null)
            {
                return default;
            }
            note.Content = command.Content;
            note.UpdatedBy = command.UpdatedBy;
            note.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<Note>().UpdateAsync(note);
            note.AddDomainEvent(new NoteUpdatedEvent(note));
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
