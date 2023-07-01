using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Interfaces;

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class CheckSprintNameHandler : IRequestHandler<CheckSprintNameQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CheckSprintNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CheckSprintNameQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.SprintRepository.CheckSprintName(query);
        }
    }
}
