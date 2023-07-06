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
using UserServices.Application.Features.Commands;
using UserServices.Application.Interfaces.IServices;
using UserServices.Application.Interfaces;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Handlers
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
            if (command is null)
            {
                return new AuthResponseDTO()
                {
                    ErrorMessage = "Invalid client request"
                };
            }

            string? accessToken = command.AccessToken;
            string? refreshToken = command.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return new AuthResponseDTO()
                {
                    ErrorMessage = "Invalid access token or refresh token"
                };
            }

            //var email = (principal.Claims.FirstOrDefault(c => c.Type == "Email").Value);
            //if(email == null)
            //{
            //    return new AuthResponseDTO()
            //    {
            //        ErrorMessage = "Email is null"
            //    };
            //}

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            var user = await _unitOfWork.UserRepository.FindUserById(Int32.Parse(userId));
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponseDTO()
                {
                    ErrorMessage = "User Not Found"
                };
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Save(cancellationToken);

            return new AuthResponseDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
