using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.SubIssues;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.SubIssues
{
    public class DeleteSubIssueHandler : IRequestHandler<DeleteSubIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSubIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteSubIssueCommand command, CancellationToken cancellationToken)
        {
            var subIssue = await _unitOfWork.SubIssueRepository.GetSubIssueById(command.Id);
            if (subIssue == null)
            {
                return default;
            }
            subIssue.IsDeleted = true;
            await _unitOfWork.Repository<SubIssue>().UpdateAsync(subIssue);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
