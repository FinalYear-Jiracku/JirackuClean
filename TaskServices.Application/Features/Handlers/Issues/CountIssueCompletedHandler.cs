using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Interfaces;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class CountIssueCompletedHandler : IRequestHandler<CountIssueCompletedQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountIssueCompletedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CountIssueCompletedQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.IssueRepository.CountIssueCompleted(query.Id);
        }
    }
}
