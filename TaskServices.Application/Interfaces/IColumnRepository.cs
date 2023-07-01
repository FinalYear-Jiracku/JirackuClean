using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Queries.Columns;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces
{
    public interface IColumnRepository
    {
        Task<List<Column>> GetColumnListBySprintId(int sprintId);
        Task<Column> GetColumnById(int id);
        Task<bool> CheckColumnName(CheckColumnNameQuery column);
    }
}
