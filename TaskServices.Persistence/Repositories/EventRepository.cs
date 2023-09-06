using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities;
using TaskServices.Persistence.Contexts;

namespace TaskServices.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public EventRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<List<EventCalendar>> CheckEventCalendar()
        {
            TimeSpan tolerance = TimeSpan.FromSeconds(30);
            var current = DateTimeOffset.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var dateTimeOffset = TimeZoneInfo.ConvertTime(current, targetTimeZone);
            var eventCalendar = await _dbContext.Events
                                      .Include(x => x.Project)
                                      .Where(r => Math.Abs((r.StartTime - dateTimeOffset).Value.TotalSeconds) < tolerance.TotalSeconds)
                                      .ToListAsync();
            return eventCalendar == null ? null : eventCalendar;
        }

        public async Task<EventCalendar> GetEventById(int id)
        {
            var findEvent = await _dbContext.Events.Include(x=>x.Project).FirstOrDefaultAsync(x => x.Id == id);
            return findEvent == null ? null : findEvent;
        }

        public async Task<List<EventCalendar>> GetEventListByProjectId(int? projectId)
        {
            var listEvent = await _dbContext.Events.Where(x => x.IsDeleted == false && x.ProjectId == projectId).ToListAsync();
            return listEvent;
        }
    }
}
