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

namespace TaskServices.Application.Features.Handlers.Columns
{
    public class GetDataColumnListHandler : IRequestHandler<GetDataColumnListQuery, List<DataColumnDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetDataColumnListHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<DataColumnDTO>> Handle(GetDataColumnListQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<List<DataColumnDTO>>($"DataColumnDTO{query.Id}");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return cacheData;
            }
            var column = await _unitOfWork.ColumnRepository.GetColumnListBySprintId(query.Id);
            var columnDto = _mapper.Map<List<DataColumnDTO>>(column);
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<DataColumnDTO>>($"DataColumnDTO{query.Id}", columnDto, expireTime);
            return columnDto;
        }
    }
}
