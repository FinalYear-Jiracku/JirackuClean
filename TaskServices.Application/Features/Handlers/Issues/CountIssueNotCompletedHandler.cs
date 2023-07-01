using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class CountIssueNotCompletedHandler : IRequestHandler<CountIssueNotCompletedQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountIssueNotCompletedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CountIssueNotCompletedQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.IssueRepository.CountIssueNotCompleted(query.Id);
        }
    }
}
