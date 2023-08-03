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
using TaskServices.Domain.Entities.Enums;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class StatisStatusHandler : IRequestHandler<StatisStatusQuery, StatisStatusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StatisStatusHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<StatisStatusDTO> Handle(StatisStatusQuery query, CancellationToken cancellationToken)
        {
            var issues = await _unitOfWork.IssueRepository.GetIssueListBySprintId(query.Id);
            if (issues == null)
            {
                return null;
            }
            var totalIssues = issues.Count();
            var completedIssues = issues.Count(x => x.Status.Name == "Completed");
            var unCompletedIssues = totalIssues - completedIssues;
            var completedPercentage = (double)completedIssues / totalIssues * 100;
            var unCompletedPercentage = (double)unCompletedIssues / totalIssues * 100;
            var result = new StatisStatusDTO
            {
                Completed = completedPercentage,
                UnCompleted = unCompletedPercentage,
            };
            return result;
        }
    }
}
