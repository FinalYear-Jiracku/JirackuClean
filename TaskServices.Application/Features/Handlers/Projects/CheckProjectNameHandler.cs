using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Interfaces;

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class CheckProjectNameHandler : IRequestHandler<CheckProjectNameQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CheckProjectNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CheckProjectNameQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProjectRepository.CheckProjectName(query);
        }
    }
}
