using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Interfaces.IServices
{
    public interface ICheckDeadlineSubIssueEventPublisher
    {
        void SendMessage(List<DeadlineSubIssuesDTO> subIssues);
        void Dispose();
    }
}
