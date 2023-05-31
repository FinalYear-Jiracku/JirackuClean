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

namespace TaskServices.Application.Features.Handlers.Projects
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProjectByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectById(query.Id);
            var projectDto = _mapper.Map<ProjectDTO>(project);
            return projectDto;
        }
    }
}
