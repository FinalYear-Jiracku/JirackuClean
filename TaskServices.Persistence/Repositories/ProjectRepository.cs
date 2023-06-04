using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ProjectRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Get Services
        public async Task<Project> GetProjectById(int id)
        {
            var project = await _connection.QueryFirstOrDefaultAsync<Project>("SELECT * FROM \"Projects\" WHERE \"IsDeleted\" = false AND \"Id\" = @Id", new { Id = id });
            return project == null ? null : project;
        }

        public async Task<List<Project>> GetProjectList()
        {
            return await _dbContext.Projects.Include(x => x.Sprints.Where(x => x.IsDeleted == false)).Where(x => x.IsDeleted == false).ToListAsync();
        }
        #endregion

        #region Check Services
        public async Task<bool> CheckProjectName(CheckProjectNameQuery project)
        {
            var findProject = await _connection.QueryFirstOrDefaultAsync<Project>("SELECT * FROM \"Projects\" WHERE \"IsDeleted\" = false AND \"Id\" <> @Id AND \"Name\" = @Name", new { Id = project.Id, Name = project.Name });
            return findProject == null ? false: true;
        }
        #endregion
    }
}
