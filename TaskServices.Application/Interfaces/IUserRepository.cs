using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface IUserRepository
    {
        Task Update(List<User> users);
    }
}
