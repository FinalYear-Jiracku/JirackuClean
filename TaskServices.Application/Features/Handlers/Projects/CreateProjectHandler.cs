using AutoMapper;
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
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Project>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var newProject = new Project()
            {
                Name = command.Name,
                CreatedBy = command.CreatedBy,
            };
            await _unitOfWork.Repository<Project>().AddAsync(newProject);
            newProject.AddDomainEvent(new ProjectCreatedEvent(newProject));
            await _unitOfWork.Save(cancellationToken);
            return newProject;
        }
    }
}
