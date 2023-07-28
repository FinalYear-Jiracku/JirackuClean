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
using TaskServices.Domain.Common.Interfaces;
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

            var findProject = await _unitOfWork.ProjectRepository.GetProjectById(newProject.Id);
            var findUser = await _unitOfWork.UserRepository.FindUserByEmail(command.CreatedBy);

            var userProject = new UserProject()
            {
                UserId = findUser.Id,
                ProjectId = findProject.Id
            };
            await _unitOfWork.UserRepository.UpdateUserProject(userProject);

            var projects = await _unitOfWork.ProjectRepository.GetProjectList(findUser.Id);
            var projectsDto = _mapper.Map<List<ProjectDTO>>(projects);
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<ProjectDTO>>($"ProjectDTO:{findUser.Email}", projectsDto, expireTime);
            return newProject;
        }
    }
}
