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
    public class NotificationRepository : INotificationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;
        public NotificationRepository(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        public async Task Add(Notification notification)
        {
            await _unitOfWork.Repository<Notification>().AddAsync(notification);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetNotificationByProjectId(int projectId)
        {
            var listNoti = await _dbContext.Notification.Where(x=> x.ProjectId == projectId).ToListAsync();
            return listNoti;
        }
    }
}
