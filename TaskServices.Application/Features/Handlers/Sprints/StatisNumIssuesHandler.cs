using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities.Enums;

namespace TaskServices.Application.Features.Handlers.Sprints
{
    public class StatisNumIssuesHandler : IRequestHandler<StatisNumIssuesQuery, List<StatisNumIssueDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StatisNumIssuesHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<StatisNumIssueDTO>> Handle(StatisNumIssuesQuery query, CancellationToken cancellationToken)
        {
            var sprints = await _unitOfWork.SprintRepository.GetSprintListByProjectId(query.Id);
            var sprintsDTO = _mapper.Map<List<StatisNumIssueDTO>>(sprints);
            return sprintsDTO;
        }
    }
}
