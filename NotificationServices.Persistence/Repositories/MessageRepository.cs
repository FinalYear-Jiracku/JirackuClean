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

        public async Task Update(int id, string content)
        {
            var findMessage = await _dbContext.Messages.FindAsync(id);
            if (findMessage == null)
            {
                return;
            }
            findMessage.Content = content;
            _dbContext.Messages.Update(findMessage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var findMessage = await _dbContext.Messages.FindAsync(id);
            if (findMessage == null)
            {
                return;
            }
            _dbContext.Messages.Remove(findMessage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessageByProjectId(int projectId)
        {
            var listMessage = await _dbContext.Messages.Include(x=>x.Group).Include(x=>x.User).Where(x => x.GroupId == projectId).ToListAsync();
            return listMessage;
        }

        public async Task<Message> GetMessageDetail(int id)
        {
            var message = await _dbContext.Messages.Include(x => x.Group).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            return message;
        }
    }
}
