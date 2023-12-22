using Google.Apis.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Interfaces.IServices;
using UserServices.Application.Interfaces;
using UserServices.Domain.Entities;
using UserServices.Application.Features.Commands.Account;

namespace UserServices.Application.Features.Handlers.Account
{
    public class RefreshHandler : IRequestHandler<RefreshCommand, AuthResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ITokenService _tokenService;

        public RefreshHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> Handle(RefreshCommand command, CancellationToken cancellationToken)
        {
            if (command.AccessToken == null || command.RefreshToken == null || command.AccessToken == "" || command.RefreshToken == "")
            {
                throw new ApplicationException("Invalid client request");
            }

            string? accessToken = command.AccessToken;
            string? refreshToken = command.RefreshToken;

            var principal = await _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var email = principal.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            if (email == null)
            {
                throw new ApplicationException("Invalid Email");
            }

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            var user = await _unitOfWork.UserRepository.FindUserById(int.Parse(userId));
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new ApplicationException("User Not Found");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);

            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Save(cancellationToken);

            return new AuthResponseDTO()
            {
                AccessToken = newAccessToken,
            };
        }
    }
}
