using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Shared.Pagination.Filter;
using TaskServices.Shared.Pagination.Helpers;
using TaskServices.Shared.Pagination.Uris;
using TaskServices.Shared.Pagination.Wrapper;

namespace TaskServices.Application.Features.Handlers.Projects
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
            if (!String.IsNullOrEmpty(query.Filter.Search))
            {
                var projectsDtoFilter = _mapper.Map<List<ProjectDTO>>(projects).Where(x => x.Name.Equals(query.Filter.Search))
                                  .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                  .Take(validFilter.PageSize).ToList();
                var countDataFilter = projectsDtoFilter.Count();
                return (projectsDtoFilter, validFilter, countDataFilter);
            }
            var projectsDto = _mapper.Map<List<ProjectDTO>>(projects)
                              .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                              .Take(validFilter.PageSize).ToList();
            var countData = projectsDto.Count();
            return (projectsDto, validFilter, countData);
        }
    }
}
