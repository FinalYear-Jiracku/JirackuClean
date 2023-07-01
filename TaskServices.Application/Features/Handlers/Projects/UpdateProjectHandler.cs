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
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public UpdateProjectHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<int> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectById(command.Id);
            if (project == null)
            {
                return default;
            }
            project.Name = command.Name;
            project.UpdatedBy = command.UpdatedBy;
            project.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<Project>().UpdateAsync(project);
            project.AddDomainEvent(new ProjectUpdatedEvent(project));
            await _unitOfWork.Save(cancellationToken);
            var projects = await _unitOfWork.ProjectRepository.GetProjectList();
            var projectsDto = _mapper.Map<List<ProjectDTO>>(projects).ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<ProjectDTO>>($"ProjectDTO", projectsDto, expireTime);
            return await Task.FromResult(0);
        }
    }
}
