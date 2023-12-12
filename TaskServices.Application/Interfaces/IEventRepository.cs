using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface IEventRepository
    {
        Task<List<EventCalendar>> GetEventListByProjectId(int? projectId);
        Task<EventCalendar> GetEventById(int id);
        Task<List<EventCalendar>> CheckEventCalendar();
    }
}
