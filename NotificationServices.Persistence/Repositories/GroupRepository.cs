using MediatR;
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
    public class GroupRepository : IGroupRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;
        public GroupRepository(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        public async Task Add(Group group)
        {
            await _unitOfWork.Repository<Group>().AddAsync(group);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Group> GetGroupDetail(int id)
        {
            var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
            return group;
        }
    }
}
