using MediatR;
using NotificationServices.Application.Features.Commands;
using NotificationServices.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Features.Handlers
{
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, int>
    {
        private readonly IMessageRepository _messageRepository;
        public DeleteMessageHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<int> Handle(DeleteMessageCommand command, CancellationToken cancellationToken)
        {
            await _messageRepository.Delete(command.Id);
            return await Task.FromResult(0);
        }
    }
}
