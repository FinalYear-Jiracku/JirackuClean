using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Commands.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class UpdateIssueHandler : IRequestHandler<UpdateIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateIssueCommand command, CancellationToken cancellationToken)
        {
            var issue = await _unitOfWork.IssueRepository.GetIssueById(command.Id);
            if (issue == null)
            {
                return default;
            }
            if(issue.SprintId != command.SprintId)
            {
                var statusToDo = await _unitOfWork.StatusRepository.GetStatusToDo(command.SprintId);
                issue.Name = command.Name;
                issue.Description = command.Description;
                issue.Type = command.Type;
                issue.Priority = command.Priority;
                issue.StoryPoint = command.StoryPoint;
                issue.StartDate = command.StartDate;
                issue.DueDate = command.DueDate;
                issue.StatusId = statusToDo.Id;
                issue.SprintId = command.SprintId;
                issue.UpdatedBy = command.UpdatedBy;
                await _unitOfWork.Repository<Issue>().UpdateAsync(issue);
                issue.AddDomainEvent(new IssueUpdatedEvent(issue));
                return await _unitOfWork.Save(cancellationToken);
            }
            issue.Name = command.Name;
            issue.Description = command.Description;
            issue.Type = command.Type;
            issue.Priority = command.Priority;
            issue.StoryPoint = command.StoryPoint;
            issue.StartDate = command.StartDate;
            issue.DueDate = command.DueDate;
            issue.StatusId = command.StatusId;
            issue.UpdatedBy = command.UpdatedBy;
            await _unitOfWork.Repository<Issue>().UpdateAsync(issue);
            issue.AddDomainEvent(new IssueUpdatedEvent(issue));
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
