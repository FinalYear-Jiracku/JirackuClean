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
    public class SubIssueRepository : ISubIssueRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public SubIssueRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Check Services
        public int CheckOrder(int? statusId)
        {
            var order = _dbContext.SubIssues.Where(x => x.StatusId == statusId).Max(x => x.Order);
            return (int)(order == null ? 0 : order);
        }
        #endregion

        #region Get Services
        public async Task<SubIssue> GetSubIssueById(int id)
        {
            var subIssue = await _dbContext.SubIssues.Include(x => x.Issue).Include(x => x.Status).Include(x => x.Comments.Where(x => x.IsDeleted == false)).FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
            return subIssue == null ? null : subIssue;
        }

        public async Task<List<SubIssue>> SubIssueNotCompleted(int? issueId)
        {
            return await _dbContext.SubIssues.Include(x => x.Status).Where(x => x.IsDeleted == false && x.IssueId == issueId && x.Status.Name != "Completed").ToListAsync();
        }
        #endregion
    }
}
