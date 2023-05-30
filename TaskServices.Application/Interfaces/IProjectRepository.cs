using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Domain.Entities;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<(List<ProjectDTO>, PaginationFilter, int)> GetProjectPagination(PaginationFilter filter);
    }
}
