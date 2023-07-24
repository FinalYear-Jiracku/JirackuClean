using AutoMapper;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Common.Interfaces;

namespace TaskServices.Infrastructure.Services
{
    public class CheckDeadlineJob : IJob
    {
        private readonly IIssueRepository _issueRepository;
        private readonly ISubIssueRepository _subIssueRepository;
        private readonly ICheckDeadlineIssueEventPublisher _checkDeadlineIssueEvent;
        private readonly ICheckDeadlineSubIssueEventPublisher _checkDeadlineSubIssueEventPublisher;
        private readonly IMapper _mapper;
        public CheckDeadlineJob(IMapper mapper,
                                IIssueRepository issueRepository, 
                                ISubIssueRepository subIssueRepository, 
                                ICheckDeadlineIssueEventPublisher checkDeadlineIssueEvent, 
                                ICheckDeadlineSubIssueEventPublisher checkDeadlineSubIssueEventPublisher)
        {
            _mapper = mapper;
            _issueRepository = issueRepository;
            _subIssueRepository = subIssueRepository;
            _checkDeadlineIssueEvent = checkDeadlineIssueEvent;
            _checkDeadlineSubIssueEventPublisher = checkDeadlineSubIssueEventPublisher;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var date = DateTimeOffset.UtcNow.AddHours(7);
            var checkDeadlineIssue = await _issueRepository.CheckDeadline(date);
            var checkDeadlineSubIssue = await _subIssueRepository.CheckDeadline(date);
            var checkDeadlineIssueDTO = _mapper.Map<List<DeadlineIssuesDTO>>(checkDeadlineIssue);
            var checkDeadlineSubIssueDTO = _mapper.Map<List<DeadlineSubIssuesDTO>>(checkDeadlineSubIssue);
            _checkDeadlineIssueEvent.SendMessage(checkDeadlineIssueDTO);
            _checkDeadlineSubIssueEventPublisher.SendMessage(checkDeadlineSubIssueDTO);
        }
    }
}
