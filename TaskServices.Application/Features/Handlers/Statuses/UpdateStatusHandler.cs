using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Features.Commands.Statuses;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Statuses
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await _unitOfWork.StatusRepository.GetStatusById(command.Id);
            if (status == null)
            {
                return default;
            }
            status.Name = command.Name;
            status.Color = command.Color;
            status.SprintId = command.SprintId;
            status.UpdatedBy = command.UpdatedBy;
            status.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<Status>().UpdateAsync(status);
            status.AddDomainEvent(new StatusUpdatedEvent(status));
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
