﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Commands.Issues;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class CreateIssueHandler : IRequestHandler<CreateIssueCommand, Issue>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public CreateIssueHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<Issue> Handle(CreateIssueCommand command, CancellationToken cancellationToken)
        {
            var order = _unitOfWork.IssueRepository.CheckOrder(command.StatusId);
            var newIssue = new Issue()
            {
                Name = command.Name,
                Type = command.Type,
                Priority = command.Priority,
                Order = order + 1,
                StartDate = command.StartDate,
                DueDate = command.DueDate,
                StatusId = command.StatusId,
                SprintId = command.SprintId,
                StoryPoint = command.StoryPoint,
                CreatedBy = command.CreatedBy,
            };
            await _unitOfWork.Repository<Issue>().AddAsync(newIssue);
            newIssue.AddDomainEvent(new IssueCreatedEvent(newIssue));
            await _unitOfWork.Save(cancellationToken);
            //var issues = await _unitOfWork.IssueRepository.GetIssueListBySprintId(command.SprintId);
            //var issuesDto = _mapper.Map<List<IssueDTO>>(issues).OrderByDescending(x => x.Id).ToList();
            //var expireTime = DateTimeOffset.Now.AddSeconds(30);
            //_cacheService.SetData<List<IssueDTO>>($"IssueDTO?sprintId={command.SprintId}&pageNumber=1&search=", issuesDto, expireTime);
            return newIssue;
        }
    }
}
