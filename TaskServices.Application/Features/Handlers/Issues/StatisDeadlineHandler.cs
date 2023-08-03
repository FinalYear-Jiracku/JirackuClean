using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class StatisDeadlineHandler : IRequestHandler<StatisDeadlineQuery, StatisDeadlineDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StatisDeadlineHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<StatisDeadlineDTO> Handle(StatisDeadlineQuery query, CancellationToken cancellationToken)
        {
            var issues = await _unitOfWork.IssueRepository.GetIssueListBySprintId(query.Id);
            if (issues == null)
            {
                return null;
            }
            var totalIssues = issues.Count();
            var currentDate = DateTimeOffset.Now;
            var overDeadlineIssues = issues.Count(x => x.DueDate < currentDate);
            var unOverDeadlineIssues = totalIssues - overDeadlineIssues;
            var overDeadlinePercentage = (double)overDeadlineIssues / totalIssues * 100;
            var unOverDeadlinePercentage = (double)unOverDeadlineIssues / totalIssues * 100;
            var result = new StatisDeadlineDTO
            {
                OverDeadline = overDeadlinePercentage,
                UnOverDeadline = unOverDeadlinePercentage,
            };
            return result;
        }
    }
}
