using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class UpgradeProjectHandler : IRequestHandler<UpgradeProjectCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public UpgradeProjectHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<int> Handle(UpgradeProjectCommand command, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectById(command.Id);
            var findUser = await _unitOfWork.UserRepository.FindUserByEmail(project.CreatedBy);
            if (project == null)
            {
                return default;
            }
            project.IsUpgraded = true;
            project.UpdatedAt = DateTimeOffset.Now;
            await _unitOfWork.Repository<Project>().UpdateAsync(project);
            project.AddDomainEvent(new ProjectUpdatedEvent(project));
            await _unitOfWork.Save(cancellationToken);
            var projects = await _unitOfWork.ProjectRepository.GetProjectList(findUser.Id);
            var projectsDto = _mapper.Map<List<ProjectDTO>>(projects).ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<ProjectDTO>>($"ProjectDTO:{findUser.Email}", projectsDto, expireTime);
            return await Task.FromResult(0);
        }
    }
}
