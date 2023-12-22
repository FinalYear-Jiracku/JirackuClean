using Microsoft.AspNetCore.SignalR;
using NotificationServices.Application.Interfaces;
using NotificationServices.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationServices.Application.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly INotificationRepository _notificationRepository;
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _socketsRoom = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        public NotificationHub(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task OnConnectedAsync(string projectId)
        {
            var claimsIdentity = Context.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Email");
            var userId = claim.Subject.Claims.ToList()[0].Value;
            if (_socketsRoom.ContainsKey(projectId))
            {
                var room = _socketsRoom[projectId];
                room.TryAdd(userId, Context.ConnectionId);
                _socketsRoom.TryAdd(projectId, room);
            }
            else
            {
                ConcurrentDictionary<string, string> _sockets = new ConcurrentDictionary<string, string>();
                _sockets.TryAdd(userId, Context.ConnectionId);
                _socketsRoom.TryAdd(projectId, _sockets);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, projectId.ToString());
        }

        public async Task OnDisconnectedAsync(string projectId)
        {
            var claimsIdentity = Context.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Email");
            var userId = claim.Subject.Claims.ToList()[0].Value;
            var room = _socketsRoom[projectId];
            room.TryRemove(userId, out _);
            if(room == null || room.Count() == 0)
            {
                _socketsRoom.TryRemove(projectId, out _);
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId.ToString());
        }

        public async Task SendMessage(string projectId, string message)
        {
            string connectionId = _socketsRoom.FirstOrDefault(x=>x.Key == projectId).Key;
            var notification = new Notification()
            {
                ProjectId = Int32.Parse(connectionId),
                Content = message
            };
            await _notificationRepository.Add(notification);

            if (connectionId != null)
            {
                await Clients.Group(connectionId).SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Console.Out.WriteLineAsync("There is no reciever");
            }
        }
    }
}
