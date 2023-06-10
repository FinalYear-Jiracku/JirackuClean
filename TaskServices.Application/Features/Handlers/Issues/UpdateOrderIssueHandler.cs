using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class UpdateOrderIssueHandler : IRequestHandler<UpdateOrderIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOrderIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateOrderIssueCommand command, CancellationToken cancellationToken)
        {
            var issues = await _unitOfWork.IssueRepository.UpdateOrderIssue(command);
            await _unitOfWork.Repository<Issue>().UpdateRangeAsync(issues);
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
