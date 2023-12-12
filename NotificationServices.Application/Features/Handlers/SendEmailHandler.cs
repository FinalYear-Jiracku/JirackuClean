using MediatR;
using NotificationServices.Application.DTOs;
using NotificationServices.Application.Features.Commands;
using NotificationServices.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var fpt = new Regex("[a-z0-9]+@fpt.edu.vn");
            var fe = new Regex("[a-z0-9]+@fe.edu.vn");

            if (!fpt.IsMatch(command.To) && !fe.IsMatch(command.To))
            {
                throw new ApplicationException("Email must be end @fpt.edu.vn or @fe.edu.vn");
            }
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
