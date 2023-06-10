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
    public class PageRepository : IPageRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public PageRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Get Services
        public async Task<Page> GetPageById(int id)
        {
            var page = await _dbContext.Pages.Include(x => x.Sprint).FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
            return page == null ? null : page;
        }

        public async Task<List<Page>> GetPageListBySprintId(int sprintId)
        {
            return await _dbContext.Pages.Include(x => x.Sprint).Where(x => x.SprintId == sprintId).ToListAsync();
        }
        public async Task<List<Page>> GetChildPageListByParentPageId(int parentPageId)
        {
            return await _dbContext.Pages.Include(x => x.Sprint).Where(x => x.ParentPageId == parentPageId).ToListAsync();
        }
        #endregion
    }
}
