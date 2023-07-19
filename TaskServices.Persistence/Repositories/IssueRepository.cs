using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public IssueRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Check Services
        public int CheckOrder(int? statusId)
        {
            var order = _dbContext.Issues.Where(x => x.StatusId == statusId).Max(x => x.Order);
            return (int)(order == null ? 0 : order);
        }

        public async Task<int> CountIssueNotCompleted(int? sprintId)
        {
            return _dbContext.Issues.Include(x => x.Status).Where(x => x.IsDeleted == false && x.SprintId == sprintId && x.Status.Name != "Completed").Count();
        }
        #endregion

        #region Update Services
        public async Task<List<Issue>> UpdateOrderIssue(UpdateOrderIssueCommand command)
        {
            var issue = await GetIssueById(command.Id);
            var issues = _dbContext.Issues.ToList();
            if (issue != null && issue.Order < command.Order)
            {
                issues = _dbContext.Issues.Where(n => n.StatusId == command.StatusId && n.Order >= issue.Order && n.Order <= command.Order).ToList();
                foreach (var changeOrder in issues)
                {
                    if (changeOrder.Id == command.Id)
                    {
                        changeOrder.Order = command.Order;
                    }
                    else
                    {
                        changeOrder.Order = changeOrder.Order - 1;
                    }
                }
                return issues;
            }
            issues = _dbContext.Issues.Where(n => n.StatusId == command.StatusId && n.Order <= issue.Order && n.Order >= command.Order).ToList();
            foreach (var changeOrder in issues)
            {
                if (changeOrder.Id == command.Id)
                {
                    changeOrder.Order = command.Order;
                }
                else
                {
                    changeOrder.Order = changeOrder.Order + 1;
                }
            }
            return issues;
        }

        public async Task<List<Issue>> DndIssue(DndIssueCommand command)
        {
            var issue = await GetIssueById(command.Id);
            var order = _dbContext.Issues.Where(x => x.StatusId == issue.StatusId).Max(x => x.Order);
            var issuesSource = _dbContext.Issues.Where(n => n.StatusId == issue.StatusId && n.Order >= issue.Order && n.Order <= order).ToList();
            issue.StatusId = command.StatusId;
            foreach (var changeOrder in issuesSource)
            {
                if (changeOrder.Id == command.Id)
                {
                    changeOrder.Order = command.Order;
                }
                else
                {
                    changeOrder.Order = changeOrder.Order - 1;
                }
            }
            order = _dbContext.Issues.Where(x => x.StatusId == command.StatusId).Max(x => x.Order);
            var issuesTarget = _dbContext.Issues.Where(n => n.StatusId == command.StatusId && n.Order >= command.Order && n.Order <= order).ToList();
            foreach (var changeOrder in issuesTarget)
            {
                changeOrder.Order = changeOrder.Order + 1;
            }
            var issues = issuesSource.Concat(issuesTarget).ToList();
            return issues;
        }
        #endregion

        #region Get Services
        public async Task<Issue> GetIssueById(int id)
        {
            var issue = await _dbContext.Issues
                             .Include(x => x.User)
                             .Include(x => x.Sprint)
                             .Include(x => x.Status)
                             .Include(x => x.Comments.Where(x => x.IsDeleted == false))
                             .Include(x => x.Attachments)
                             .Include(x => x.SubIssues.Where(x => x.IsDeleted == false))
                             .ThenInclude(sub => sub.User)
                             .Include(x => x.SubIssues.Where(x => x.IsDeleted == false))
                             .ThenInclude(sub => sub.Status)
                             .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
            return  issue == null ? null : issue;
        }

        public async Task<List<Issue>> GetIssueListBySprintId(int? sprintId)
        {
            return await _dbContext.Issues.Where(x => x.IsDeleted == false && x.SprintId == sprintId).Include(x => x.User).Include(x=>x.Sprint).Include(x=>x.Status).Include(x => x.SubIssues.Where(x => x.IsDeleted == false)).ToListAsync();
        }

        public async Task<List<Issue>> IssueNotCompleted(int? sprintId)
        {
            return await _dbContext.Issues.Include(x => x.Status).Include(x => x.SubIssues.Where(x => x.IsDeleted == false)).Where(x => x.IsDeleted == false && x.SprintId == sprintId && x.Status.Name != "Completed").ToListAsync();
        }
        #endregion
    }
}
