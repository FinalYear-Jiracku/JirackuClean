using NotificationServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Interfaces
{
    public interface IMessageRepository
    {
        Task Add(Message message);
        Task<List<Message>> GetMessageByProjectId(int projectId);
    }
}
