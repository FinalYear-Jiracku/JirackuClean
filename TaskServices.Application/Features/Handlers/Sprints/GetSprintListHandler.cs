using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class GetSprintListHandler : IRequestHandler<GetSprintListQuery, (List<SprintDTO>, PaginationFilter, int)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetSprintListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<(List<SprintDTO>, PaginationFilter, int)> Handle(GetSprintListQuery query, CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(query.Filter.PageNumber, query.Filter.PageSize, query.Filter.Search, query.Filter.Type, query.Filter.Priority, query.Filter.StatusId, query.Filter.UserId);
            var cacheData = _cacheService.GetData<List<SprintDTO>>($"SprintDTO?projectId={query.Id}&pageNumber={query.Filter.PageNumber}&search={query.Filter.Search}");
            var sprints = await _unitOfWork.SprintRepository.GetSprintListByProjectId(query.Id);
            var sprintsDto = _mapper.Map<List<SprintDTO>>(sprints);
            if (cacheData != null && cacheData.Count() > 0)
            {
                return (cacheData, validFilter, sprints.Count());
            }
            if (!String.IsNullOrEmpty(query.Filter.Search))
            {
                sprintsDto = _mapper.Map<List<SprintDTO>>(sprints).Where(x => x.Name.Contains(query.Filter.Search)).OrderByDescending(x => x.Id)
                                  .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                  .Take(validFilter.PageSize).ToList();
                return (sprintsDto, validFilter, _mapper.Map<List<SprintDTO>>(sprints).Count());
            }
            sprintsDto = _mapper.Map<List<SprintDTO>>(sprints).OrderByDescending(x => x.Id)
                              .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                              .Take(validFilter.PageSize).ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<SprintDTO>>($"SprintDTO?projectId={query.Id}&pageNumber={query.Filter.PageNumber}&search={query.Filter.Search}", sprintsDto, expireTime);
            return (sprintsDto, validFilter, sprints.Count());
        }
    }
}
