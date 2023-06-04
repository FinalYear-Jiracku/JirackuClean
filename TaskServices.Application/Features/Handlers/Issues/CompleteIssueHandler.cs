using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Features.Commands.Sprints;
using TaskServices.Application.Features.Commands.Statuses;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class CompleteIssueHandler : IRequestHandler<CompleteIssueCommand, List<Issue>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompleteIssueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Issue>> Handle(CompleteIssueCommand command, CancellationToken cancellationToken)
        {
            var sprint = await _unitOfWork.SprintRepository.GetSprintById(command.SprintId);
            var issueNotComplete = await _unitOfWork.IssueRepository.IssueNotCompleted(command.SprintId);
            var statusToDo = await _unitOfWork.StatusRepository.GetStatusToDo(command.SprintIdToUpdate);
            if (command.SprintIdToUpdate == 0)
            {
                var newSprint = new Sprint()
                {
                    Name = "New Sprint",
                    ProjectId = sprint.ProjectId,
                    CreatedBy = "Dinh Gia Bao",
                };

                newSprint.Statuses = new List<Status>();
                var status1 = new Status()
                {
                    Name = "ToDo",
                    Color = "#dcfce7",
                    CreatedBy = newSprint.CreatedBy,
                };
                status1.AddDomainEvent(new StatusCreatedEvent(status1));
                newSprint.Statuses.Add(status1);

                var status2 = new Status()
                {
                    Name = "InProgress",
                    Color = "#fee2e2",
                    CreatedBy = newSprint.CreatedBy,
                };
                status2.AddDomainEvent(new StatusCreatedEvent(status2));
                newSprint.Statuses.Add(status2);

                var status3 = new Status()
                {
                    Name = "Completed",
                    Color = "#e0f2fe",
                    CreatedBy = newSprint.CreatedBy,
                };
                status3.AddDomainEvent(new StatusCreatedEvent(status3));
                newSprint.Statuses.Add(status3);

                newSprint.Columns = new List<Column>();
                var column1 = new Column
                {
                    Name = "Problem",
                    Color = "#dcfce7",
                    CreatedBy = newSprint.CreatedBy,
                };
                newSprint.Columns.Add(column1);

                var column2 = new Column
                {
                    Name = "Try",
                    Color = "#dcfce7",
                    CreatedBy = newSprint.CreatedBy,
                };
                newSprint.Columns.Add(column2);

                var column3 = new Column
                {
                    Name = "Keep",
                    Color = "#dcfce7",
                    CreatedBy = newSprint.CreatedBy,
                };
                newSprint.Columns.Add(column3);

                await _unitOfWork.Repository<Sprint>().AddAsync(newSprint);
                newSprint.AddDomainEvent(new SprintUpdatedEvent(newSprint));
                await _unitOfWork.Save(cancellationToken);

                statusToDo = await _unitOfWork.StatusRepository.GetStatusToDo(newSprint.Id);
                foreach (var issue in issueNotComplete)
                {
                    issue.SprintId = newSprint.Id;
                    issue.StatusId = statusToDo.Id;
                }
                await _unitOfWork.Repository<Issue>().UpdateRangeAsync(issueNotComplete);
                await _unitOfWork.Save(cancellationToken);
                return issueNotComplete;
            }
            foreach (var issue in issueNotComplete)
            {
                issue.SprintId = command.SprintIdToUpdate;
                issue.StatusId = statusToDo.Id;
            }
            await _unitOfWork.Repository<Issue>().UpdateRangeAsync(issueNotComplete);
            await _unitOfWork.Save(cancellationToken);
            return issueNotComplete;
        }
    }
}
