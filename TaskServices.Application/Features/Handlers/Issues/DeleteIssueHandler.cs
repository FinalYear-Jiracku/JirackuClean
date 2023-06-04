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
    public class DeleteIssueHandler : IRequestHandler<DeleteIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteIssueCommand command, CancellationToken cancellationToken)
        {
            var issue = await _unitOfWork.IssueRepository.GetIssueById(command.Id);
            if (issue == null)
            {
                return default;
            }
            issue.IsDeleted = true;
            await _unitOfWork.Repository<Issue>().UpdateAsync(issue);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
