using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;
using TaskServices.Persistence.Services;
using TaskServices.Shared.Pagination.Filter;
using TaskServices.Shared.Pagination.Helpers;
using TaskServices.Shared.Pagination.Uris;
using TaskServices.Shared.Pagination.Wrapper;

namespace TaskServices.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _configuration;

        public ProjectRepository(ApplicationDbContext dbContext, ICacheService cacheService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Get Services
        public async Task<Project> GetProjectById(int id)
        {
            var cacheData = _cacheService.GetData<Project>("projects");
            if (cacheData != null)
            {
                return cacheData;
            }
            cacheData = await _connection.QueryFirstOrDefaultAsync<Project>("SELECT * FROM \"Projects\" WHERE \"IsDeleted\" = false AND \"Id\" = @Id", new { Id = id });
            if (cacheData == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<Project>($"projects{id}", cacheData, expireTime);
            return cacheData;
        }

        public async Task<List<Project>> GetProjectList()
        {
            var cacheData = _cacheService.GetData<List<Project>>("projects");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return cacheData;
            }
            var projects = await _connection.QueryAsync<Project>("SELECT * FROM \"Projects\" WHERE \"IsDeleted\" = false");
            List<Project> asList = projects.ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<Project>>("projects", asList, expireTime);
            return asList;
        }
        #endregion

        #region Check Services
        public async Task<bool> CheckProjectName(CheckProjectNameQuery project)
        {
            var findProject = await _connection.QueryFirstOrDefaultAsync<Project>("SELECT * FROM \"Projects\" WHERE \"IsDeleted\" = false AND \"Id\" <> @Id AND \"Name\" = @Name", new { Id = project.Id, Name = project.Name });
            if (findProject == null)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
