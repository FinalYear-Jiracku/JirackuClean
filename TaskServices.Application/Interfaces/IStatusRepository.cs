using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Features.Queries.Statuses;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetStatusListBySprintId(int sprintId);
        Task<Status> GetStatusById(int? id);
        Task<bool> CheckStatusName(CheckStatusNameQuery status);
        Task<Status> GetStatusToDo(int? sprintId);
    }
}
