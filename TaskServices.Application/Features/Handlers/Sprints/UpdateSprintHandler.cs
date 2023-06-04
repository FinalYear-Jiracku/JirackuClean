using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class UpdateSprintHandler : IRequestHandler<UpdateSprintCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSprintHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateSprintCommand command, CancellationToken cancellationToken)
        {
            var sprint = await _unitOfWork.SprintRepository.GetSprintById(command.Id);
            if (sprint == null)
            {
                return default;
            }
            sprint.Name = command.Name;
            sprint.StartDate = command.StartDate;
            sprint.EndDate = command.EndDate;
            sprint.UpdatedBy = command.UpdatedBy;
            await _unitOfWork.Repository<Sprint>().UpdateAsync(sprint);
            sprint.AddDomainEvent(new SprintUpdatedEvent(sprint));
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
