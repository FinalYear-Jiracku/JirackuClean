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
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class StatisTypeHandler : IRequestHandler<StatisTypeQuery, StatisTypeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public StatisTypeHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<StatisTypeDTO> Handle(StatisTypeQuery query, CancellationToken cancellationToken)
        {
            var issues = await _unitOfWork.IssueRepository.GetIssueListBySprintId(query.Id);
            if(issues == null)
            {
                return null;
            }
            var totalIssues = issues.Count();
            var taskIssues = issues.Count(x => x.Type == IssueType.Task);
            var bugIssues = totalIssues - taskIssues;
            var taskPercentage = (double)taskIssues / totalIssues * 100;
            var bugPercentage = (double)bugIssues / totalIssues * 100;
            var result = new StatisTypeDTO
            {
                Task = taskPercentage,
                Bug = bugPercentage,
            };
            return result;
        }
    }
}
