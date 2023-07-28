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
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Statuses
{
    public class GetStatusByIdHandler : IRequestHandler<GetStatusByIdQuery, StatusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetStatusByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<StatusDTO> Handle(GetStatusByIdQuery query, CancellationToken cancellationToken)
        {
            //var cacheData = _cacheService.GetData<StatusDTO>($"StatusDTO{query.Id}");
            //if (cacheData != null)
            //{
            //    return cacheData;
            //}
            var status = await _unitOfWork.StatusRepository.GetStatusById(query.Id);
            var statusDto = _mapper.Map<StatusDTO>(status);
            if (statusDto == null)
            {
                return null;
            }
            //var expireTime = DateTimeOffset.Now.AddSeconds(30);
            //_cacheService.SetData<StatusDTO>($"StatusDTO{statusDto.Id}", statusDto, expireTime);
            return statusDto;
        }
    }
}
