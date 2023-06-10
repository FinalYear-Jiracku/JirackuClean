using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Pipelines.Sockets.Unofficial.Arenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public SprintRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        #region Check Services
        public async Task<bool> CheckSprintName(CheckSprintNameQuery sprint)
        {
            var findSprint = await _connection.QueryFirstOrDefaultAsync<Sprint>("SELECT * FROM \"Sprints\" WHERE \"IsDeleted\" = false AND \"Id\" <> @Id AND \"Name\" = @Name", new { Id = sprint.Id, Name = sprint.Name });
            return findSprint == null ? false : true;
        }

        public string GenerateUniqueSprintName(string baseName)
        {
            var existingNames = _dbContext.Sprints.Where(x => x.Name.StartsWith(baseName)).Select(x => x.Name).ToList();
            var index = 1;
            var uniqueName = baseName;
            while (existingNames.Contains(uniqueName))
            {
                uniqueName = $"{baseName} {index}";
                index++;
            }
            return uniqueName;
        }
        #endregion

        #region Get Services
        public async Task<Sprint> GetSprintById(int id)
        {
            var sprint = await _connection.QueryFirstOrDefaultAsync<Sprint>("SELECT * FROM \"Sprints\" WHERE \"IsDeleted\" = false AND \"Id\" = @Id", new { Id = id });
            return sprint == null ? null : sprint;
        }

        public async Task<List<Sprint>> GetSprintListByProjectId(int projectId)
        {
            return await _dbContext.Sprints.Where(x => x.IsDeleted == false && x.ProjectId == projectId).ToListAsync();
        }
        #endregion
    }
}
