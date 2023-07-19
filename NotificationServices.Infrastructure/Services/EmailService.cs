using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using NotificationServices.Application.DTOs;
using NotificationServices.Application.Interfaces.IServices;
using NotificationServices.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly INotificationEventPulisher _notificationEventPulisher;

        public EmailService(IConfiguration config, INotificationEventPulisher notificationEventPulisher)
        {
            _config = config;
            _notificationEventPulisher = notificationEventPulisher;
        }
        public async Task SendEmail(EmailDTO request)
        {
            var inviteToken = Guid.NewGuid().ToString();
            var acceptInviteLink = $"http://localhost:3000/accept-invite/{inviteToken}";
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_config.GetSection("Name").Value, _config.GetSection("Email").Value);
            email.From.Add(new MailboxAddress(_config.GetSection("Name").Value, _config.GetSection("Email").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = string.Format("<p>Bạn đã nhận được lời mời tham gia vào dự án {0}</p>", request.Body)
            + $"<p>Hãy nhấp vào <a href=\"{acceptInviteLink}\">Accept Invite</a> để chấp nhận lời mời.</p>";
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("Host").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("Email").Value, _config.GetSection("Password").Value);
            await smtp.SendAsync(email);
            _notificationEventPulisher.SendMessage(request.To, request.ProjectId, inviteToken);
            smtp.Disconnect(true);
        }
    }
}
