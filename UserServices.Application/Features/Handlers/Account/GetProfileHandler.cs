using AutoMapper;
using Google.Apis.Auth;
using MediatR;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Queries;
using UserServices.Application.Interfaces;
using UserServices.Application.Interfaces.IServices;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Handlers.Account
{
    public class GetProfileHandler : IRequestHandler<GetProfileQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetProfileQuery queries, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.FindUserById(queries.Id);
            if (user == null)
            {
                throw new ApplicationException("User Not Found");
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }
    }
}
