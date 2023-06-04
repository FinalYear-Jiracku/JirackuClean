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
    public class CreateStatusHandler : IRequestHandler<CreateStatusCommand, Status>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Status> Handle(CreateStatusCommand command, CancellationToken cancellationToken)
        {
            var newStatus = new Status()
            {
                Name = command.Name,
                Color = command.Color,
                SprintId = command.SprintId,
                CreatedBy = command.CreatedBy,
            };
            await _unitOfWork.Repository<Status>().AddAsync(newStatus);
            newStatus.AddDomainEvent(new StatusCreatedEvent(newStatus));
            await _unitOfWork.Save(cancellationToken);
            return newStatus;
        }
    }
}
