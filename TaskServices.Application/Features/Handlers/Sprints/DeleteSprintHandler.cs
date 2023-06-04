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
    public class DeleteSprintHandler : IRequestHandler<DeleteSprintCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSprintHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteSprintCommand command, CancellationToken cancellationToken)
        {
            var sprint = await _unitOfWork.SprintRepository.GetSprintById(command.Id);
            if (sprint == null)
            {
                return default;
            }
            sprint.IsDeleted = true;
            await _unitOfWork.Repository<Sprint>().UpdateAsync(sprint);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
