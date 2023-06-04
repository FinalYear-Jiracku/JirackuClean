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

namespace TaskServices.Application.Features.Handlers.Issues
{
    public class GetIssueListHandler : IRequestHandler<GetIssueListQuery, List<IssueDTO>>
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
        public async Task<List<IssueDTO>> Handle(GetIssueListQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<List<IssueDTO>>($"IssueDTO{query.Id}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return cacheData;
            }
            var status = await _unitOfWork.IssueRepository.GetIssueListBySprintId(query.Id);
            var statusDto = _mapper.Map<List<IssueDTO>>(status).ToList();
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<IssueDTO>>($"IssueDTO{query.Id}", statusDto, expireTime);
            return statusDto;
        }
    }
}
