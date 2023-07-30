using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Statuses;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public StatusRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        #region Check Services
        public async Task<bool> CheckStatusName(CheckStatusNameQuery status)
        {
            var findStatus = await _connection.QueryFirstOrDefaultAsync<Status>("SELECT * FROM \"Statuses\" WHERE \"IsDeleted\" = false AND \"Id\" <> @Id AND \"SprintId\" = @SprintId AND \"Name\" = @Name", new { Id = status.Id, Name = status.Name, SprintId = status.SprintId });
            return findStatus == null ? false : true;
        }
        #endregion

        #region Get Services
        public async Task<Status> GetStatusById(int? id)
        {
            var status = await _connection.QueryFirstOrDefaultAsync<Status>("SELECT * FROM \"Statuses\" WHERE \"IsDeleted\" = false AND \"Id\" = @Id", new { Id = id });
            return status == null ? null : status;
        }

        public async Task<List<Status>> GetStatusListBySprintId(int sprintId)
        {
             return await _dbContext.Statuses.Include(x=>x.Sprint)
                                             .Include(x => x.Issues.Where(x => x.IsDeleted == false).OrderBy(x => x.Order)).ThenInclude(x=>x.User)
                                             .Include(x => x.SubIssues.Where(x => x.IsDeleted == false))
                                             .Where(x => x.IsDeleted == false && x.SprintId == sprintId).ToListAsync();
        }

        public async Task<Status> GetStatusToDo(int? sprintId)
        {
            return await _dbContext.Statuses.FirstOrDefaultAsync(x => x.IsDeleted == false && x.SprintId == sprintId && x.Name == "ToDo");
        }
        #endregion
    }
}
