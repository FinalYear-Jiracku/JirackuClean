using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Events;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class GetListDeadlineIssueHandler : IRequestHandler<GetListDeadLineIssueQuery, List<DeadlineIssuesDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetListDeadlineIssueHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<DeadlineIssuesDTO>> Handle(GetListDeadLineIssueQuery query, CancellationToken cancellationToken)
        {
            var date = DateTimeOffset.UtcNow.AddHours(7);
            var findIssues = await _unitOfWork.IssueRepository.ListDeadLineIssue(query.Id, date);
            var findIssuesDTO = _mapper.Map<List<DeadlineIssuesDTO>>(findIssues);
            return findIssuesDTO;
        }
    }
}
