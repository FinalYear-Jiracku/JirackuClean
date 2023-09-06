using MailKit.Security;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using NotificationServices.Application.DTOs;
using NotificationServices.Application.Interfaces.IServices;
using NotificationServices.Application.Interfaces;
using NotificationServices.Application.Messages;
using NotificationServices.Application.SignalR;
using NotificationServices.Domain.Common.Interfaces;
using NotificationServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IConfiguration _config;
        private readonly INotificationEventPulisher _notificationEventPulisher;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<NotificationHub> _hubContext;

        public EventService(IConfiguration config, INotificationEventPulisher notificationEventPulisher, IServiceProvider serviceProvider, IHubContext<NotificationHub> hubContext)
        {
            _config = config;
            _notificationEventPulisher = notificationEventPulisher;
            _serviceProvider = serviceProvider;
            _hubContext = hubContext;
        }

        public async Task SendEvent(List<EventCalendar> request)
        {
            foreach (var eventCalendar in request)
            {
                var startTime = eventCalendar.StartTime;
                var endTime = eventCalendar.EndTime;
                int startHour = startTime?.Hour + 7 ?? 0;
                int endHour = endTime?.Hour + 7 ?? 0;
                string content = string.Format("Today you have an event {0} that will take place from {1} o'clock to {2} o'clock", eventCalendar.Title, startHour, endHour);
                using var scope = _serviceProvider.CreateScope();
                var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var noti = new Notification()
                {
                    ProjectId = eventCalendar.ProjectId,
                    Content = content,
                };
                await notificationRepository.Add(noti);
                await _hubContext.Clients.Groups(eventCalendar.ProjectId.ToString()).SendAsync("ReceiveMessage", content);
            }
        }
    }
}
