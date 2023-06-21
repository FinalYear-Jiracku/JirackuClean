using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Columns;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Columns
{
    public class GetColumnByIdHandler : IRequestHandler<GetColumnByIdQuery, ColumnDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetColumnByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ColumnDTO> Handle(GetColumnByIdQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<ColumnDTO>($"ColumnDTO{query.Id}");
            if (cacheData != null)
            {
                return cacheData;
            }
            var column = await _unitOfWork.ColumnRepository.GetColumnById(query.Id);
            var columnDto = _mapper.Map<ColumnDTO>(column);
            if (columnDto == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<ColumnDTO>($"ColumnDTO{columnDto.Id}", columnDto, expireTime);
            return columnDto;
        }
    }
}
