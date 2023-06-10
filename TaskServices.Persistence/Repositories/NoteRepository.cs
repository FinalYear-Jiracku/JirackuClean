using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Notes;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public NoteRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Check Services
        public int CheckOrder(int? columnId)
        {
            var order = _dbContext.Notes.Where(x => x.ColumnId == columnId).Max(x => x.Order);
            return (int)(order == null ? 0 : order);
        }
        #endregion

        #region Get Services
        public async Task<Note> GetNoteById(int id)
        {
            var note = await _dbContext.Notes.Include(x => x.Comments.Where(x => x.IsDeleted == false)).FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
            return note == null ? null : note;
        }
        #endregion

        #region Update Services
        public async Task<List<Note>> UpdateOrderNote(UpdateOrderNoteCommand command)
        {
            var note = await GetNoteById(command.Id);
            var notes = _dbContext.Notes.ToList();
            if (note != null && note.Order < command.Order)
            {
                notes = _dbContext.Notes.Where(n => n.ColumnId == command.ColumnId && n.Order >= note.Order && n.Order <= command.Order).ToList();
                foreach (var changeOrder in notes)
                {
                    if (changeOrder.Id == command.Id)
                    {
                        changeOrder.Order = command.Order;
                    }
                    else
                    {
                        changeOrder.Order = changeOrder.Order - 1;
                    }
                }
                return notes;
            }
            notes = _dbContext.Notes.Where(n => n.ColumnId == command.ColumnId && n.Order <= note.Order && n.Order >= command.Order).ToList();
            foreach (var changeOrder in notes)
            {
                if (changeOrder.Id == command.Id)
                {
                    changeOrder.Order = command.Order;
                }
                else
                {
                    changeOrder.Order = changeOrder.Order + 1;
                }
            }
            return notes;
        }
        #endregion
    }
}
