using Dapper;
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
    public class CommentRepository : ICommentRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public CommentRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await _connection.QueryFirstOrDefaultAsync<Comment>("SELECT * FROM \"Comments\" WHERE \"IsDeleted\" = false AND \"Id\" = @Id", new { Id = id });
            return comment == null ? null : comment;
        }
    }
}
