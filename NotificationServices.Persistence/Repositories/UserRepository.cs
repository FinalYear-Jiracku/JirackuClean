using Microsoft.EntityFrameworkCore;
using NotificationServices.Application.Interfaces;
using NotificationServices.Domain.Entities;
using NotificationServices.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        public async Task Add(User user)
        {
            await _unitOfWork.Repository<User>().AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserDetail(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
    }
}
