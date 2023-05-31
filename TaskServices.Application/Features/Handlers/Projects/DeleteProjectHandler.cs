using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectById(command.Id);
            if (project == null)
            {
                return default;
            }
            project.IsDeleted = true;
            await _unitOfWork.Repository<Project>().UpdateAsync(project);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
