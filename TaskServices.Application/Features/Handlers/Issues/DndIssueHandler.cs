using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class DndIssueHandler : IRequestHandler<DndIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DndIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DndIssueCommand command, CancellationToken cancellationToken)
        {
            var issues = await _unitOfWork.IssueRepository.DndIssue(command);
            await _unitOfWork.Repository<Issue>().UpdateRangeAsync(issues);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
