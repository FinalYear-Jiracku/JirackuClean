using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Statuses;
using TaskServices.Application.Interfaces;
using TaskServices.Domain.Entities.Enums;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Handlers.Statuses
{
    public class GetDataStatusListHandler : IRequestHandler<GetDataStatusListQuery, (List<DataStatusDTO>, PaginationFilter, int)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetDataStatusListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<(List<DataStatusDTO>, PaginationFilter, int)> Handle(GetDataStatusListQuery query, CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(query.Filter.PageNumber, query.Filter.PageSize, query.Filter.Search, query.Filter.Type, query.Filter.Priority, query.Filter.StatusId, query.Filter.UserId);
            var cacheData = _cacheService.GetData<List<DataStatusDTO>>($"DataStatusDTO{query.Id}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return (cacheData, validFilter, cacheData.Count());
            }
            var status = await _unitOfWork.StatusRepository.GetStatusListBySprintId(query.Id);
            var statusDto = _mapper.Map<List<DataStatusDTO>>(status);
            if (!String.IsNullOrEmpty(query.Filter.Search))
            {
                statusDto = _mapper.Map<List<DataStatusDTO>>(status).Select(dto =>
                {
                    dto.Issues = dto.Issues.Where(issue => issue.Name.Contains(query.Filter.Search)).ToList();
                    return dto;
                }).Where(dto => dto.Issues != null && dto.Issues.Any()).ToList();
                if(query.Filter.Type == IssueType.Bug || query.Filter.Type == IssueType.Task)
                {
                    statusDto = _mapper.Map<List<DataStatusDTO>>(status).Select(dto =>
                    {
                        dto.Issues = dto.Issues.Where(issue => issue.Type.Equals(query.Filter.Type) &&
                                                      issue.Name.Contains(query.Filter.Search)).ToList();
                        return dto;
                    }).Where(dto => dto.Issues != null && dto.Issues.Any()).ToList();
                    return (statusDto, validFilter, statusDto.Count());
                }
                if (query.Filter.Priority == IssuePriority.Low || query.Filter.Priority == IssuePriority.Normal || 
                    query.Filter.Priority == IssuePriority.High || query.Filter.Priority == IssuePriority.Urgent)
                {
                    statusDto = _mapper.Map<List<DataStatusDTO>>(status).Select(dto =>
                    {
                        dto.Issues = dto.Issues.Where(issue => issue.Priority.Equals(query.Filter.Priority) &&
                                                      issue.Name.Contains(query.Filter.Search)).ToList();
                        return dto;
                    }).Where(dto => dto.Issues != null && dto.Issues.Any()).ToList();
                    return (statusDto, validFilter, statusDto.Count());
                }
                if (query.Filter.StatusId != null)
                {
                    statusDto = _mapper.Map<List<DataStatusDTO>>(status).Select(dto =>
                    {
                        dto.Issues = dto.Issues.Where(issue => issue.Status.Id.Equals(query.Filter.StatusId) && 
                                                      issue.Name.Contains(query.Filter.Search)).ToList();
                        return dto;
                    }).Where(dto => dto.Issues != null && dto.Issues.Any()).ToList();
                    return (statusDto, validFilter, statusDto.Count());
                }
                if (query.Filter.UserId != null)
                {
                    statusDto = _mapper.Map<List<DataStatusDTO>>(status).Select(dto =>
                    {
                        dto.Issues = dto.Issues.Where(issue => issue.UserIssues.Equals(query.Filter.UserId) &&
                                                      issue.Name.Contains(query.Filter.Search)).ToList();
                        return dto;
                    }).Where(dto => dto.Issues != null && dto.Issues.Any()).ToList();
                    return (statusDto, validFilter, statusDto.Count());
                }
                return (statusDto, validFilter, statusDto.Count());
            }
            if (query.Filter.Type == IssueType.Bug || query.Filter.Type == IssueType.Task)
            {
                statusDto = _mapper.Map<List<DataStatusDTO>>(status).Select(dto =>
                {
                    dto.Issues = dto.Issues.Where(issue => issue.Type.Equals(query.Filter.Type)).ToList();
                    return dto;
                }).Where(dto => dto.Issues != null && dto.Issues.Any()).ToList();
                return (statusDto, validFilter, statusDto.Count());
            }
            if (query.Filter.Priority == IssuePriority.Low || query.Filter.Priority == IssuePriority.Normal ||
                query.Filter.Priority == IssuePriority.High || query.Filter.Priority == IssuePriority.Urgent)
            {
                statusDto = _mapper.Map<List<DataStatusDTO>>(status).Select(dto =>
                {
                    dto.Issues = dto.Issues.Where(issue => issue.Priority.Equals(query.Filter.Priority)).ToList();
                    return dto;
                }).Where(dto => dto.Issues != null && dto.Issues.Any()).ToList();
                return (statusDto, validFilter, statusDto.Count());
            }
            if (query.Filter.StatusId != null)
            {
                statusDto = _mapper.Map<List<DataStatusDTO>>(status).Where(dto => dto.Id.Equals(query.Filter.StatusId)).ToList();
                return (statusDto, validFilter, statusDto.Count());
            }
            if (query.Filter.UserId != null)
            {
                statusDto = _mapper.Map<List<DataStatusDTO>>(status).Select(dto =>
                {
                    dto.Issues = dto.Issues.Where(issue => issue.UserIssues.Equals(query.Filter.UserId)).ToList();
                    return dto;
                }).Where(dto => dto.Issues != null && dto.Issues.Any()).ToList();
                return (statusDto, validFilter, statusDto.Count());
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<DataStatusDTO>>($"DataStatusDTO{query.Id}", statusDto, expireTime);
            return (statusDto, validFilter, statusDto.Count());
        }
    }
}
