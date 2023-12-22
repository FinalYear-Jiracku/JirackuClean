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
    public class StatisUserHandler : IRequestHandler<StatisUserQuery, List<StatisUserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StatisUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<StatisUserDTO>> Handle(StatisUserQuery query, CancellationToken cancellationToken)
        {
            var currentYear = DateTimeOffset.UtcNow.Year;
            var users = await _unitOfWork.Repository<User>().GetAllAsync();
            var usersDTO = users.Where(u => u.CreatedAt.HasValue && u.CreatedAt.Value.Year == currentYear)
                            .GroupBy(u => u.CreatedAt.Value.Month)
                            .OrderBy(g => g.Key)
                            .Select(g => new StatisUserDTO
                            {
                                Month = g.Key,
                                Users = g.Count()
                            }).ToList();
            if (query.Year != 0)
            {
                usersDTO = users.Where(u => u.CreatedAt.HasValue && u.CreatedAt.Value.Year == query.Year)
                            .GroupBy(u => u.CreatedAt.Value.Month)
                            .OrderBy(g => g.Key)
                            .Select(g => new StatisUserDTO
                            {
                                Month = g.Key,
                                Users = g.Count()
                            }).ToList();
                return usersDTO;
            }
            return usersDTO;
        }
    }
}
