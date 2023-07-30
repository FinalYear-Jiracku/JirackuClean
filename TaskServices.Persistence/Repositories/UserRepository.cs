using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Pipelines.Sockets.Unofficial.Arenas;
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

        public async Task<User> FindUserByEmail(string email)
        {
            await using var context = new ApplicationDbContext(_dbContext);
            var findUser = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return findUser == null ? null : findUser;
        }

        public async Task<User> FindUserById(int? userId)
        {
            await using var context = new ApplicationDbContext(_dbContext);
            var findUser = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return findUser == null ? null : findUser;
        }

        public async Task<User> GetUserByInviteToken(string inviteToken)
        {
            await using var context = new ApplicationDbContext(_dbContext);
            var findUser = await context.Users.FirstOrDefaultAsync(u => u.InviteToken == inviteToken);
            return findUser == null ? null : findUser;
        }

        public async Task<List<User>> GetListUserByProjectId(int projectId)
        {
            await using var context = new ApplicationDbContext(_dbContext);
            var users = await (from u in context.Users
                               join up in context.UserProject on u.Id equals up.UserId
                               join p in context.Projects on up.ProjectId equals p.Id
                               where up.ProjectId == projectId
                               select u).ToListAsync();
            return users;
        }

        public async Task Update(List<User> users)
        {
            foreach (var user in users)
            {
                await using var context = new ApplicationDbContext(_dbContext);
                var existedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
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

        public async Task UpdateUserProject(UserProject userProject)
        {
            await using var context = new ApplicationDbContext(_dbContext);
            context.UserProject.Add(userProject);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserProjectList(List<UserProject> userProjects)
        {
            foreach (var user in userProjects)
            {
                await using var context = new ApplicationDbContext(_dbContext);
                context.UserProject.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
