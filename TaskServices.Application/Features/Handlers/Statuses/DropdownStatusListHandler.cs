using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Sprints;
using TaskServices.Application.Features.Queries.Statuses;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Shared.Pagination.Filter;

namespace TaskServices.Application.Features.Handlers.Statuses
{
    public class DropdownStatusListHandler : IRequestHandler<DropdownStatusListQuery, List<StatusDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public DropdownStatusListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<StatusDTO>> Handle(DropdownStatusListQuery query, CancellationToken cancellationToken)
        {
            //var cacheData = _cacheService.GetData<List<StatusDTO>>($"StatusDTO{query.Id}");
            //if (cacheData != null && cacheData.Count() > 0)
            //{
            //    return cacheData;
            //}
            var status = await _unitOfWork.StatusRepository.GetStatusListBySprintId(query.Id);
            var statusDto = _mapper.Map<List<StatusDTO>>(status).ToList();
            //var expireTime = DateTimeOffset.Now.AddSeconds(30);
            //_cacheService.SetData<List<StatusDTO>>($"StatusDTO{query.Id}", statusDto, expireTime);
            return statusDto;
        }
    }
}
