using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface IIssueRepository
    {
        int CheckOrder(int? statusId);
        Task<List<Issue>> UpdateOrderIssue(UpdateOrderIssueCommand command);
        Task<List<Issue>> DndIssue(DndIssueCommand command);
        Task<int> CountIssueNotCompleted(int? sprintId);
        Task<int> CountIssueCompleted(int? sprintId);
        Task<List<Issue>> IssueNotCompleted(int? sprintId);
        Task<List<Issue>> GetIssueListBySprintId(int? sprintId);
        Task<List<Issue>> GetStatisBySprintId(int? sprintId);
        Task<Issue> GetIssueById(int id);
        Task<Attachment> FindAttachment(int issueId);
        Task<List<Issue>> CheckDeadline(DateTimeOffset dateTimeOffset);
        Task<List<Issue>> ListDeadLineIssue(int projectid, DateTimeOffset dateTimeOffset);
    }
}
