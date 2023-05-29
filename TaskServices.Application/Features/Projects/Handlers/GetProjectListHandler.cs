using AutoMapper;
using KPTMockProject.Common.Filter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Projects.Queries;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Projects.Handlers
{
    public class GetProjectListHandler : IRequestHandler<GetProjectListQuery, (List<ProjectDTO>, PaginationFilter, int)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProjectListHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(List<ProjectDTO>, PaginationFilter, int)> Handle(GetProjectListQuery query, CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(query.Filter.PageNumber, query.Filter.PageSize, query.Filter.Search);
            var projects = await _unitOfWork.Repository<Project>().GetAllAsync();
            var projectsDto = _mapper.Map<List<ProjectDTO>>(projects)
                              .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                              .Take(validFilter.PageSize).ToList();
            var countData = projectsDto.Count();
            return (projectsDto, validFilter, countData);
        }
    }
}
