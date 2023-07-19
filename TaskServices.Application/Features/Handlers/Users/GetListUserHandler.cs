using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.DTOs;
using TaskServices.Application.Features.Queries.Users;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Application.Features.Handlers.Users
{
    public class GetListUserHandler : IRequestHandler<GetListUserQuery, List<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetListUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<List<UserDTO>> Handle(GetListUserQuery query, CancellationToken cancellationToken)
        {
            var cacheData = _cacheService.GetData<List<UserDTO>>($"UserDTO?projectId={query.Id}");
            if (cacheData != null)
            {
                return cacheData;
            }
            var user = await _unitOfWork.UserRepository.GetListUserByProjectId(query.Id);
            var userDTO = _mapper.Map<List<UserDTO>>(user);
            if (userDTO == null)
            {
                return null;
            }
            var expireTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData<List<UserDTO>>($"UserDTO?projectId={query.Id}", userDTO, expireTime);
            return userDTO;
        }
    }
}
