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
    public class MessageRepository : IMessageRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;
        public MessageRepository(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        public async Task Add(Message message)
        {
            await _unitOfWork.Repository<Message>().AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessageByProjectId(int projectId)
        {
            var listMessage = await _dbContext.Messages.Include(x=>x.Group).Include(x=>x.User).Where(x => x.GroupId == projectId).ToListAsync();
            return listMessage;
        }
    }
}
