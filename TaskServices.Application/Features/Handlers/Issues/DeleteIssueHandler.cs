using AutoMapper;
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
    public class DeleteIssueHandler : IRequestHandler<DeleteIssueCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public DeleteIssueHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<int> Handle(DeleteIssueCommand command, CancellationToken cancellationToken)
        {
            var issue = await _unitOfWork.IssueRepository.GetIssueById(command.Id);
            if (issue == null)
            {
                return default;
            }
            issue.IsDeleted = true;
            await _unitOfWork.Repository<Issue>().UpdateAsync(issue);
            await _unitOfWork.Save(cancellationToken);
            //var issueList = await _unitOfWork.IssueRepository.GetIssueListBySprintId(issue.SprintId);
            //var issueDtoList = _mapper.Map<List<IssueDTO>>(issueList).OrderByDescending(x => x.Id).ToList();
            //var expireTime = DateTimeOffset.Now.AddSeconds(30);
            //_cacheService.SetData<List<IssueDTO>>($"IssueDTO?sprintId={issue.SprintId}&pageNumber=1&search=", issueDtoList, expireTime);
            return await Task.FromResult(0);
        }
    }
}
