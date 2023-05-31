using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectById(command.Id);
            if(project == null)
            {
                return default;
            }
            project.Name = command.Name;
            await _unitOfWork.Repository<Project>().UpdateAsync(project);
            project.AddDomainEvent(new ProjectUpdatedEvent(project));
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
