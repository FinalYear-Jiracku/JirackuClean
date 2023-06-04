using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetProjectByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ProjectDTO> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<ProjectDTO>($"ProjectDTO{query.Id}");
            if (cacheData != null)
            {
                return cacheData;
            }
            var project = await _unitOfWork.ProjectRepository.GetProjectById(query.Id);
            var projectDto = _mapper.Map<ProjectDTO>(project);
            if(projectDto == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<ProjectDTO>($"ProjectDTO{projectDto.Id}", projectDto, expireTime);
            return projectDto;
        }
    }
}
