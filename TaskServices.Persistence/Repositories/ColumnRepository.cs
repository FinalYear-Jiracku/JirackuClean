using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Columns;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ColumnRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        #region Check Services
        public async Task<bool> CheckColumnName(CheckColumnNameQuery column)
        {
            var findColumn = await _connection.QueryFirstOrDefaultAsync<Column>("SELECT * FROM \"Columns\" WHERE \"IsDeleted\" = false AND \"Id\" <> @Id AND \"Name\" = @Name", new { Id = column.Id, Name = column.Name });
            return findColumn == null ? false : true;
        }
        #endregion

        #region Get Services
        public async Task<Column> GetColumnById(int id)
        {
            var note = await _dbContext.Columns.Include(x => x.Notes.Where(x => x.IsDeleted == false)).FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
            return note == null ? null : note;
        }

        public async Task<List<Column>> GetColumnListBySprintId(int sprintId)
        {
            return await _dbContext.Columns.Include(x => x.Sprint).Include(x => x.Notes.Where(x => x.IsDeleted == false).OrderBy(x => x.Order)).ThenInclude(x => x.Comments.Where(x => x.IsDeleted == false)).Where(x => x.SprintId == sprintId).ToListAsync();
        }
        #endregion
    }
}
