using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Domain.Entities;

namespace UserServices.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindUser(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);
        Task<User> FindUserById(int userId);
    }
}
