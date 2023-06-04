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

namespace TaskServices.Application.Features.Handlers.Statuses
{
    public class GetDataStatusListHandler : IRequestHandler<GetDataStatusListQuery, List<DataStatusDTO>>
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
        public async Task<List<DataStatusDTO>> Handle(GetDataStatusListQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<List<DataStatusDTO>>($"DataStatusDTO{query.Id}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return cacheData;
            }
            var status = await _unitOfWork.StatusRepository.GetStatusListBySprintId(query.Id);
            var statusDto = _mapper.Map<List<DataStatusDTO>>(status);
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<DataStatusDTO>>($"DataStatusDTO{query.Id}", statusDto, expireTime);
            return statusDto;
        }
    }
}
