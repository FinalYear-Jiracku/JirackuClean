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
    public class CreateIssueHandler : IRequestHandler<CreateIssueCommand, Issue>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Issue> Handle(CreateIssueCommand command, CancellationToken cancellationToken)
        {
            var order = _unitOfWork.IssueRepository.CheckOrder(command.StatusId);
            var newIssue = new Issue()
            {
                Name = command.Name,
                Type = command.Type,
                Priority = command.Priority,
                Order = order + 1,
                StartDate = command.StartDate,
                DueDate = command.DueDate,
                StatusId = command.StatusId,
                SprintId = command.SprintId,
                StoryPoint = command.StoryPoint,
                CreatedBy = command.CreatedBy,
            };
            await _unitOfWork.Repository<Issue>().AddAsync(newIssue);
            newIssue.AddDomainEvent(new IssueCreatedEvent(newIssue));
            await _unitOfWork.Save(cancellationToken);
            return newIssue;
        }
    }
}
