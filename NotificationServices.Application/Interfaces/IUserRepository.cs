using NotificationServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetUserDetail(int id);
    }
}
