using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Queries;
using UserServices.Application.Interfaces;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Handlers.Account
{
    public class GetYearHandler : IRequestHandler<GetYearQuery, List<YearDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetYearHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<YearDTO>> Handle(GetYearQuery query, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Repository<User>().GetAllAsync();
            var yearDTO = users.Where(u => u.CreatedAt.HasValue)
                            .GroupBy(u => u.CreatedAt.Value.Year)
                            .OrderByDescending(g => g.Key)
                            .Select(g => new YearDTO
                            {
                                Year = g.Key
                            }).ToList();
            return yearDTO;
        }
    }
}
