using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface IPageRepository
    {
        Task<List<Page>> GetPageListBySprintId(int sprintId);
        Task<List<Page>> GetChildPageListByParentPageId(int parentPageId);
        Task<Page> GetPageById(int id);
    }
}
