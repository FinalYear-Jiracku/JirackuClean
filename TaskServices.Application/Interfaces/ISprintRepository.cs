using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface ISprintRepository
    {
        Task<List<Sprint>> GetSprintListByProjectId(int? projectId);
        Task<Sprint> GetSprintById(int id);
        Task<bool> CheckSprintName(CheckSprintNameQuery sprint);
        string GenerateUniqueSprintName(string baseName);
    }
}
