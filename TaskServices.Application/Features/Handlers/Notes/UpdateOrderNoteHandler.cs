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
    public class UpdateOrderNoteHandler : IRequestHandler<UpdateOrderNoteCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOrderNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateOrderNoteCommand command, CancellationToken cancellationToken)
        {
            var notes = await _unitOfWork.NoteRepository.UpdateOrderNote(command);
            await _unitOfWork.Repository<Note>().UpdateRangeAsync(notes);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
