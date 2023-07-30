using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Columns;
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
            //var statusToDo = await _unitOfWork.StatusRepository.GetStatusToDo(command.SprintIdToUpdate);
            //var order = _unitOfWork.IssueRepository.CheckOrder(statusToDo.Id);
            var i = 1;
            if (command.SprintIdToUpdate == 0)
            {
                var baseName = "New Sprint";
                var uniqueName = _unitOfWork.SprintRepository.GenerateUniqueSprintName(baseName);
                var newSprint = new Sprint()
                {
                    Name = uniqueName,
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
                    Name = "Done",
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
                column1.AddDomainEvent(new ColumnCreatedEvent(column1));
                newSprint.Columns.Add(column1);

                var column2 = new Column
                {
                    Name = "Try",
                    Color = "#dcfce7",
                    CreatedBy = newSprint.CreatedBy,
                };
                column2.AddDomainEvent(new ColumnCreatedEvent(column2));
                newSprint.Columns.Add(column2);

                var column3 = new Column
                {
                    Name = "Keep",
                    Color = "#dcfce7",
                    CreatedBy = newSprint.CreatedBy,
                };
                column3.AddDomainEvent(new ColumnCreatedEvent(column3));
                newSprint.Columns.Add(column3);

                await _unitOfWork.Repository<Sprint>().AddAsync(newSprint);
                newSprint.AddDomainEvent(new SprintUpdatedEvent(newSprint));
                await _unitOfWork.Save(cancellationToken);

                //statusToDo = await _unitOfWork.StatusRepository.GetStatusToDo(newSprint.Id);
                foreach (var issue in issueNotComplete)
                {
                    issue.SprintId = newSprint.Id;
                    issue.Status = null;
                    issue.Order = i++;
                    var subIssueNotCompleteForNewSprint = await _unitOfWork.SubIssueRepository.SubIssueNotCompleted(issue.Id);
                    foreach (var subIssue in subIssueNotCompleteForNewSprint)
                    {
                        subIssue.StatusId = null;
                    }
                }
                await _unitOfWork.Repository<Issue>().UpdateRangeAsync(issueNotComplete);
                sprint.IsCompleted = true;
                await _unitOfWork.Repository<Sprint>().UpdateAsync(sprint);
                await _unitOfWork.Save(cancellationToken);
                return issueNotComplete;
            }
            foreach (var issue in issueNotComplete)
            {
                //if (order != 0)
                //{
                //    issue.SprintId = command.SprintIdToUpdate;
                //    issue.Status = statusToDo.Id;
                //    issue.Order = order++;
                //}
                //else
                //{
                //    issue.SprintId = command.SprintIdToUpdate;
                //    issue.StatusId = statusToDo.Id;
                //    issue.Order = i++;
                //}
                issue.SprintId = command.SprintIdToUpdate;
                issue.Status = null;
                var subIssueNotCompleteForExistSprint = await _unitOfWork.SubIssueRepository.SubIssueNotCompleted(issue.Id);
                foreach (var subIssue in subIssueNotCompleteForExistSprint)
                {
                    subIssue.Status = null;
                }
            }
            await _unitOfWork.Repository<Issue>().UpdateRangeAsync(issueNotComplete);
            sprint.IsCompleted = true;
            await _unitOfWork.Repository<Sprint>().UpdateAsync(sprint);
            await _unitOfWork.Save(cancellationToken);
            return issueNotComplete;
        }
    }
}
