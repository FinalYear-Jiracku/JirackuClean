using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface ISubIssueRepository
    {
        int CheckOrder(int? statusId);
        Task<SubIssue> GetSubIssueById(int id);
        Task<List<SubIssue>> SubIssueNotCompleted(int? issueId);
        Task<List<SubIssue>> GetSubIssueListByIssueId(int? issueId);
        Task<List<SubIssue>> CheckDeadline(DateTimeOffset dateTimeOffset);
    }
}
