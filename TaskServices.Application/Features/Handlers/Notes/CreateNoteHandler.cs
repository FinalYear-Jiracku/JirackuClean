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
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, Note>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Note> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            var order = _unitOfWork.NoteRepository.CheckOrder(command.ColumnId);
            var newNote = new Note()
            {
                Content = command.Content,
                ColumnId = command.ColumnId,
                Order = order + 1,
                CreatedBy = command.CreatedBy,
            };
            await _unitOfWork.Repository<Note>().AddAsync(newNote);
            newNote.AddDomainEvent(new NoteCreatedEvent(newNote));
            await _unitOfWork.Save(cancellationToken);
            return newNote;
        }
    }
}
