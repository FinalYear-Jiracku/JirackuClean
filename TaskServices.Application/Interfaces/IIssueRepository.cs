using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface IIssueRepository
    {
        int CheckOrder(int? statusId);
        Task<int> CountIssueNotCompleted(int? sprintId);
        Task<List<Issue>> IssueNotCompleted(int? sprintId);
        Task<List<Issue>> GetIssueListBySprintId(int sprintId);
        Task<Issue> GetIssueById(int id);
    }
}
