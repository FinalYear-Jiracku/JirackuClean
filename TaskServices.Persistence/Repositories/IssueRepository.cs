using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #region Get Services
        public async Task<Issue> GetIssueById(int id)
        {
            var issue = await _dbContext.Issues.Include(x => x.Sprint).Include(x => x.Status).Include(x => x.SubIssues.Where(x => x.IsDeleted == false)).FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
            return  issue == null ? null : issue;
        }

        public async Task<List<Issue>> GetIssueListBySprintId(int sprintId)
        {
            return await _dbContext.Issues.Where(x => x.IsDeleted == false && x.SprintId == sprintId).Include(x=>x.Sprint).Include(x=>x.Status).Include(x => x.SubIssues.Where(x => x.IsDeleted == false)).ToListAsync();
        }

        public async Task<List<Issue>> IssueNotCompleted(int? sprintId)
        {
            return await _dbContext.Issues.Include(x => x.Status).Where(x => x.IsDeleted == false && x.SprintId == sprintId && x.Status.Name != "Completed").ToListAsync();
        }
        #endregion
    }
}
