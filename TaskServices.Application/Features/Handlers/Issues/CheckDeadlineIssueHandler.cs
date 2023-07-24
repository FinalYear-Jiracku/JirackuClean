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
using TaskServices.Domain.Entities;
using TaskServices.Domain.Entities.Enums;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class CheckDeadlineIssueHandler : IRequestHandler<CheckDeadlineIssueQuery, List<IssueDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CheckDeadlineIssueHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<IssueDTO>> Handle(CheckDeadlineIssueQuery query, CancellationToken cancellationToken)
        {
            var date = DateTimeOffset.UtcNow.AddHours(7);
            var issues = await _unitOfWork.IssueRepository.CheckDeadline(date);
            var issuesDTO = _mapper.Map<List<IssueDTO>>(issues);
            return issuesDTO;
        }
    }
}
