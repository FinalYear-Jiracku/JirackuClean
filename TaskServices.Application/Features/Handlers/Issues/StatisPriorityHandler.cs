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
    public class StatisPriorityHandler : IRequestHandler<StatisPriorityQuery, StatisPriorityDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StatisPriorityHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<StatisPriorityDTO> Handle(StatisPriorityQuery query, CancellationToken cancellationToken)
        {
            var issues = await _unitOfWork.IssueRepository.GetStatisBySprintId(query.Id);
            if (issues == null)
            {
                return null;
            }
            var totalIssues = issues.Count();
            var urgentIssues = issues.Count(x => x.Priority == IssuePriority.Urgent);
            var highIssues = issues.Count(x => x.Priority == IssuePriority.High);
            var normalIssues = issues.Count(x => x.Priority == IssuePriority.Normal);
            var lowIssues = issues.Count(x => x.Priority == IssuePriority.Low);
            var urgentPercentage = (double)urgentIssues / totalIssues * 100;
            var highPercentage = (double)highIssues / totalIssues * 100;
            var normalPercentage = (double)normalIssues / totalIssues * 100;
            var lowPercentage = (double)lowIssues / totalIssues * 100;
            var result = new StatisPriorityDTO
            {
                Urgent = urgentPercentage,
                High = highPercentage,
                Normal = normalPercentage,
                Low = lowPercentage,
            };
            return result;
        }
    }
}
