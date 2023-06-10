using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Commands.SubIssues;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.SubIssues
{
    public class CreateSubIssueHandler : IRequestHandler<CreateSubIssueCommand, SubIssue>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateSubIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SubIssue> Handle(CreateSubIssueCommand command, CancellationToken cancellationToken)
        {
            var order = _unitOfWork.SubIssueRepository.CheckOrder(command.StatusId);
            var newIssue = new SubIssue()
            {
                Name = command.Name,
                Type = command.Type,
                Priority = command.Priority,
                Order = order + 1,
                StartDate = command.StartDate,
                DueDate = command.DueDate,
                StatusId = command.StatusId,
                IssueId = command.IssueId,
                StoryPoint = command.StoryPoint,
                CreatedBy = command.CreatedBy,
            };
            await _unitOfWork.Repository<SubIssue>().AddAsync(newIssue);
            newIssue.AddDomainEvent(new SubIssueCreatedEvent(newIssue));
            await _unitOfWork.Save(cancellationToken);
            return newIssue;
        }
    }
}
