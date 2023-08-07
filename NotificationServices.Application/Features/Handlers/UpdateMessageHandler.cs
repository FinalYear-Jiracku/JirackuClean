using MediatR;
using NotificationServices.Application.DTOs;
using NotificationServices.Application.Features.Commands;
using NotificationServices.Application.Interfaces;
using NotificationServices.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Features.Handlers
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, int>
    {
        private readonly IMessageRepository _messageRepository;
        public UpdateMessageHandler(IMessageRepository messageRepository)
        {
           _messageRepository = messageRepository;
        }
        public async Task<int> Handle(UpdateMessageCommand command, CancellationToken cancellationToken)
        {
            await _messageRepository.Update(command.Id,command.Content);
            return await Task.FromResult(0);
        }
    }
}
