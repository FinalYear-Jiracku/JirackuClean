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
        private readonly IUriService _uriService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ProjectRepository(ApplicationDbContext dbContext, ICacheService cacheService, IUriService uriService, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
            _uriService = uriService;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            _mapper = mapper;

        }

        public async Task<(List<ProjectDTO>, PaginationFilter, int)> GetProjectPagination(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Search);
            //var cacheData = _cacheService?.GetData<List<ProjectDTO>>("projects");
            //if (cacheData != null && cacheData.Count() > 0)
            //{
            //    return (cacheData, validFilter, cacheData.Count());
            //}
            //var projects = await _connection.QueryAsync<Project>("SELECT * FROM \"Projects\"");
            var projects = await _dbContext.Projects.ToListAsync();
            var asList = _mapper.Map<List<ProjectDTO>>(projects).ToList();
            var countResults = asList.Count();
            //var expireTime = DateTimeOffset.Now.AddSeconds(30);
            //_cacheService.SetData<List<ProjectDTO>>("projects", asList, expireTime);
            return (asList, validFilter, countResults);
        }
    }
}
