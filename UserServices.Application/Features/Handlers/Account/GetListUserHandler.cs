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
using UserServices.Shared.Pagination.Filter;

namespace UserServices.Application.Features.Handlers.Account
{
    public class GetListUserHandler : IRequestHandler<GetListUserQuery, (List<UserDTO>, PaginationFilter, int)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetListUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(List<UserDTO>, PaginationFilter, int)> Handle(GetListUserQuery query, CancellationToken cancellationToken)
        {
            var validFilter = new PaginationFilter(query.Filter.PageNumber, query.Filter.PageSize, query.Filter.Search);
            var users = await _unitOfWork.Repository<User>().GetAllAsync();
            var userDTO = _mapper.Map<List<UserDTO>>(users).Where(x => x.Role == "User")
                         .OrderByDescending(x => x.Id)
                         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                         .Take(validFilter.PageSize).ToList();
            if (!string.IsNullOrEmpty(query.Filter.Search))
            {
                userDTO = _mapper.Map<List<UserDTO>>(users).Where(x => x.Role == "User" && x.Email.Contains(query.Filter.Search))
                         .OrderByDescending(x => x.Id)
                         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                         .Take(validFilter.PageSize).ToList();
                return (userDTO, validFilter, userDTO.Count());
            }
            return (userDTO, validFilter, users.Count());
        }
    }
}
