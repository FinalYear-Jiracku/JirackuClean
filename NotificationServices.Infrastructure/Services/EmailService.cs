using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using MimeKit.Text;
using NotificationServices.Application.DTOs;
using NotificationServices.Application.Interfaces;
using NotificationServices.Application.Interfaces.IServices;
using NotificationServices.Application.Messages;
using NotificationServices.Domain.Common.Interfaces;
using NotificationServices.Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
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
        private readonly IServiceProvider _serviceProvider;

        public EmailService(IConfiguration config, INotificationEventPulisher notificationEventPulisher, IServiceProvider serviceProvider)
        {
            _config = config;
            _notificationEventPulisher = notificationEventPulisher;
            _serviceProvider = serviceProvider;
        }

        public async Task SendEmailDeadlineIssue(List<DeadlineIssue> request)
        {
            foreach (var issue in request)
            {
                var email = new MimeMessage();
                email.Sender = new MailboxAddress(_config.GetSection("Name").Value, _config.GetSection("Email").Value);
                email.From.Add(new MailboxAddress(_config.GetSection("Name").Value, _config.GetSection("Email").Value));
                email.To.Add(MailboxAddress.Parse(issue.User));
                email.Subject = "Jiracku Report";
                DateTimeOffset currentDateTime = DateTimeOffset.UtcNow;
                TimeSpan timeSpan = currentDateTime - issue.DueDate;
                int numberOfDays = timeSpan.Days;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = string.Format("<p>Sprint: {0}. The deadline for {1} is currently approaching on {2}. Currently, you are {3} days late</p>", issue.Sprint, issue.Name, issue.DueDate, numberOfDays);
                email.Body = bodyBuilder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("Host").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("Email").Value, _config.GetSection("Password").Value);
                await smtp.SendAsync(email);
                string emailContent = string.Format("Sprint: {0}. The deadline for {1} is currently approaching on {2}. Currently, you are {3} days late", issue.Sprint, issue.Name, issue.DueDate, numberOfDays);
                using var scope = _serviceProvider.CreateScope();
                var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var noti = new Notification()
                {
                    ProjectId = issue.ProjectId,
                    Content = emailContent,
                };
                await notificationRepository.Add(noti);
                smtp.Disconnect(true);
            }
        }

        public async Task SendEmailDeadlineSubIssue(List<DeadlineSubIssue> request)
        {
            foreach (var subIssue in request)
            {
                var email = new MimeMessage();
                email.Sender = new MailboxAddress(_config.GetSection("Name").Value, _config.GetSection("Email").Value);
                email.From.Add(new MailboxAddress(_config.GetSection("Name").Value, _config.GetSection("Email").Value));
                email.To.Add(MailboxAddress.Parse(subIssue.User));
                email.Subject = "Jiracku Report";
                DateTimeOffset currentDateTime = DateTimeOffset.UtcNow;
                TimeSpan timeSpan = currentDateTime - subIssue.DueDate;
                int numberOfDays = timeSpan.Days;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = string.Format("<p>Sprint: {0}. The deadline for {1} of Issue: {2} is currently approaching on {3}. Currently, you are {4} days late</p>", subIssue.Sprint, subIssue.Name, subIssue.Issue, subIssue.DueDate, numberOfDays);
                email.Body = bodyBuilder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("Host").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("Email").Value, _config.GetSection("Password").Value);
                await smtp.SendAsync(email);
                string emailContent = string.Format("Sprint: {0}. The deadline for {1} of Issue: {2} is currently approaching on {3}. Currently, you are {4} days late", subIssue.Sprint, subIssue.Name, subIssue.Issue, subIssue.DueDate, numberOfDays);
                using var scope = _serviceProvider.CreateScope();
                var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var noti = new Notification()
                {
                    ProjectId = subIssue.ProjectId,
                    Content = emailContent,
                };
                await notificationRepository.Add(noti);
                smtp.Disconnect(true);
            }
        }

        public async Task SendEmailInvite(EmailDTO request)
        {
            var inviteToken = Guid.NewGuid().ToString();
            var acceptInviteLink = $"http://localhost:3000/accept-invite/{inviteToken}";
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_config.GetSection("Name").Value, _config.GetSection("Email").Value);
            email.From.Add(new MailboxAddress(_config.GetSection("Name").Value, _config.GetSection("Email").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = string.Format("<p>You have received an invitation to join the project {0}</p>", request.Body)
                + $"<p>Click <a href=\"{acceptInviteLink}\">Accept Invite</a> to accept the invitation.</p>";
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
