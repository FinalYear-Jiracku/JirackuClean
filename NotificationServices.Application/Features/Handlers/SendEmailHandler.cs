using MediatR;
using NotificationServices.Application.DTOs;
using NotificationServices.Application.Features.Commands;
using NotificationServices.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Features.Handlers
{
    public class SendEmailHandler : IRequestHandler<SendEmaiCommand, int>
    {
        private readonly IEmailService _emailService;
        public SendEmailHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<int> Handle(SendEmaiCommand command, CancellationToken cancellationToken)
        {
            var emailDTO = new EmailDTO
            {
                To = command.To,
                Subject = command.Subject,
                Body = command.Body,
                ProjectId = command.ProjectId
            };
            await _emailService.SendEmailInvite(emailDTO);
            return await Task.FromResult(0);
        }
    }
}
