using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Projects;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContext;

        public UserRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Update(List<User> users)
        {
            foreach (var user in users)
            {
                await using var context = new ApplicationDbContext(_dbContext);
                var existedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
                if (existedUser != null)
                {
                    existedUser.Name = user.Name;
                    existedUser.Email = user.Email;
                    existedUser.Image = user.Image;
                }
                else
                {
                    context.Users.Add(user);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
