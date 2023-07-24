using NotificationServices.Application.DTOs;
using NotificationServices.Application.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendEmailInvite(EmailDTO request);
        Task SendEmailDeadlineIssue(List<DeadlineIssue> request);
        Task SendEmailDeadlineSubIssue(List<DeadlineSubIssue> request);
    }
}
