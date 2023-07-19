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
        Task<User> FindUserByEmail(string email);
        Task<User> FindUserById(int? userId);
        Task<List<User>> GetListUserByProjectId(int projectId);
        Task<User> GetUserByInviteToken(string inviteToken);
        Task Update(List<User> users);
        Task UpdateUserProjectList(List<UserProject> userProjects);
        Task UpdateUserProject(UserProject userProject);
    }
}
