using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Statuses;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Statuses
{
    public class DeleteStatusHandler : IRequestHandler<DeleteStatusCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await _unitOfWork.StatusRepository.GetStatusById(command.Id);
            if (status == null)
            {
                return default;
            }
            status.IsDeleted = true;
            await _unitOfWork.Repository<Status>().UpdateAsync(status);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
