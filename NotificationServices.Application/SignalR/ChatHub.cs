using Microsoft.AspNetCore.SignalR;
using NotificationServices.Application.Interfaces;
using NotificationServices.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _socketsRoom = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        public ChatHub(IGroupRepository groupRepository, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _groupRepository = groupRepository; 
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task OnConnectedAsync(string projectId)
        {
            var claimsIdentity = Context.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Email");
            var userId = claim.Subject.Claims.ToList()[0].Value;
            var email = claim.Subject.Claims.ToList()[1].Value;
            var findUser = await _userRepository.GetUserDetail(Int32.Parse(userId));
            if(findUser == null)
            {
                var user = new User()
                {
                    Id = Int32.Parse(userId),
                    UserName = email
                };
                await _userRepository.Add(user);
            }
            if (_socketsRoom.ContainsKey(projectId))
            {
                var room = _socketsRoom[projectId];
                room.TryAdd(userId, Context.ConnectionId);
                _socketsRoom.TryAdd(projectId, room);
            }
            else
            {
                var findGroup = await _groupRepository.GetGroupDetail(Int32.Parse(projectId));
                if (findGroup == null)
                {
                    var group = new Group()
                    {
                        Id = Int32.Parse(projectId)
                        
                    };
                    await _groupRepository.Add(group);
                }
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
            if (room == null || room.Count() == 0)
            {
                _socketsRoom.TryRemove(projectId, out _);
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, projectId.ToString());
        }

        public async Task SendMessage(string projectId, string message)
        {
            var claimsIdentity = Context.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Email");
            var userId = claim.Subject.Claims.ToList()[0].Value;
            string connectionId = _socketsRoom.FirstOrDefault(x => x.Key == projectId).Key;

            if(message != "Update Message" && message != "Delete Message") 
            {
                var newMessage = new Message()
                {
                    GroupId = Int32.Parse(connectionId),
                    UserId = Int32.Parse(userId),
                    Content = message
                };
                await _messageRepository.Add(newMessage);
            }
                
            if (connectionId != null)
            {
                await Clients.Group(connectionId).SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Console.Out.WriteLineAsync("Không tin thấy người nhận");
            }
        }

    }
}
