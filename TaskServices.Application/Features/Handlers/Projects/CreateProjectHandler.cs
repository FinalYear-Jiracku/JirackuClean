using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Project>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public CreateProjectHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
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
            var projects = await _unitOfWork.ProjectRepository.GetProjectList();
            var projectsDto = _mapper.Map<List<ProjectDTO>>(projects);
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<ProjectDTO>>($"ProjectDTO", projectsDto, expireTime);
            return newProject;
        }
    }
}
