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
        private readonly ICacheService _cacheService;

        public GetProjectListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<(List<ProjectDTO>, PaginationFilter, int)> Handle(GetProjectListQuery query, CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(query.Filter.PageNumber, query.Filter.PageSize, query.Filter.Search);
            var cacheData = _cacheService.GetData<List<ProjectDTO>>($"ProjectDTO{query.Filter.PageNumber}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return (cacheData,validFilter,cacheData.Count());
            }
            var projects = await _unitOfWork.ProjectRepository.GetProjectList();
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
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<ProjectDTO>>($"ProjectDTO{query.Filter.PageNumber}", projectsDto, expireTime);
            return (projectsDto, validFilter, countData);
        }
    }
}
