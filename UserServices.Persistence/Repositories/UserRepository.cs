using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.Interfaces;
using UserServices.Domain.Entities;
using UserServices.Persistence.Contexts;

namespace UserServices.Infrastructure.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> FindUserById(int userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted == false);
        }

        public async Task<User> FindUser(GoogleJsonWebSignature.Payload payload)
        {
            var user = await _dbContext.Users.Where(x => x.Email == payload.Email).FirstOrDefaultAsync();
            return user == null ? null : user;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
