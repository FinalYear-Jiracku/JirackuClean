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
            var validFilter = new PaginationFilter(query.Filter.PageNumber, query.Filter.PageSize, query.Filter.Search);
            var cacheData = _cacheService.GetData<List<SprintDTO>>($"SprintDTO{query.Id}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return (cacheData, validFilter, cacheData.Count());
            }
            var sprints = await _unitOfWork.SprintRepository.GetSprintListByProjectId(query.Id);
            if (!String.IsNullOrEmpty(query.Filter.Search))
            {
                var sprintsDtoFilter = _mapper.Map<List<SprintDTO>>(sprints).Where(x => x.Name.Equals(query.Filter.Search))
                                  .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                  .Take(validFilter.PageSize).ToList();
                var countDataFilter = sprintsDtoFilter.Count();
                return (sprintsDtoFilter, validFilter, countDataFilter);
            }
            var sprintsDto = _mapper.Map<List<SprintDTO>>(sprints)
                              .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                              .Take(validFilter.PageSize).ToList();
            var countData = sprintsDto.Count();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<SprintDTO>>($"SprintDTO{query.Id}", sprintsDto, expireTime);
            return (sprintsDto, validFilter, countData);
        }
    }
}
