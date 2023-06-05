using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Issues;
using TaskServices.Application.Features.Queries.Statuses;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities.Enums;
using TaskServices.Domain.Entities;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class GetIssueListHandler : IRequestHandler<GetIssueListQuery, (List<IssueDTO>, PaginationFilter, int)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetIssueListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<(List<IssueDTO>, PaginationFilter, int)> Handle(GetIssueListQuery query, CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(query.Filter.PageNumber, query.Filter.PageSize, query.Filter.Search, query.Filter.Type, query.Filter.Priority, query.Filter.StatusId, query.Filter.UserId);
            var cacheData = _cacheService.GetData<List<IssueDTO>>($"IssueDTO{query.Id}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return (cacheData, validFilter, cacheData.Count());
            }
            var issue = await _unitOfWork.IssueRepository.GetIssueListBySprintId(query.Id);
            var issueDto = _mapper.Map<List<IssueDTO>>(issue);
            if (!String.IsNullOrEmpty(query.Filter.Search))
            {
                issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.Name.Contains(query.Filter.Search)).ToList();
                if (query.Filter.Type == IssueType.Bug || query.Filter.Type == IssueType.Task)
                {
                    issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.Name.Contains(query.Filter.Search) && 
                                                                        dto.Type.Equals(query.Filter.Type)).ToList();
                    return (issueDto, validFilter, issueDto.Count());
                }
                if (query.Filter.Priority == IssuePriority.Low || query.Filter.Priority == IssuePriority.Normal ||
                    query.Filter.Priority == IssuePriority.High || query.Filter.Priority == IssuePriority.Urgent)
                {
                    issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.Name.Contains(query.Filter.Search) && 
                                                                        dto.Priority.Equals(query.Filter.Priority)).ToList();
                    return (issueDto, validFilter, issueDto.Count());
                }
                if (query.Filter.StatusId != null)
                {
                    issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.Name.Contains(query.Filter.Search) && 
                                                                        dto.Status.Id.Equals(query.Filter.StatusId)).ToList();
                    return (issueDto, validFilter, issueDto.Count());
                }
                if (query.Filter.UserId != null)
                {
                    issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.Name.Contains(query.Filter.Search) && 
                                                                        dto.UserIssues.Equals(query.Filter.UserId)).ToList();
                    return (issueDto, validFilter, issueDto.Count());
                }
                return (issueDto, validFilter, issueDto.Count());
            }
            if (query.Filter.Type == IssueType.Bug || query.Filter.Type == IssueType.Task)
            {
                issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.Type.Equals(query.Filter.Type)).ToList();
                return (issueDto, validFilter, issueDto.Count());
            }
            if (query.Filter.Priority == IssuePriority.Low || query.Filter.Priority == IssuePriority.Normal ||
                query.Filter.Priority == IssuePriority.High || query.Filter.Priority == IssuePriority.Urgent)
            {
                issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.Priority.Equals(query.Filter.Priority)).ToList();
                return (issueDto, validFilter, issueDto.Count());
            }
            if (query.Filter.StatusId != null)
            {
                issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.Status.Id.Equals(query.Filter.StatusId)).ToList();
                return (issueDto, validFilter, issueDto.Count());
            }
            if (query.Filter.UserId != null)
            {
                issueDto = _mapper.Map<List<IssueDTO>>(issue).Where(dto => dto.UserIssues.Equals(query.Filter.UserId)).ToList();
                return (issueDto, validFilter, issueDto.Count());
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<IssueDTO>>($"IssueDTO{query.Id}", issueDto, expireTime);
            return (issueDto, validFilter, issueDto.Count());
        }
    }
}
