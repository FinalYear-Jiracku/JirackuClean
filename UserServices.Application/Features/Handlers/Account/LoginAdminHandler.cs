using BCrypt.Net;
using Google.Apis.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Commands.Account;
using UserServices.Application.Interfaces;
using UserServices.Application.Interfaces.IServices;

namespace UserServices.Application.Features.Handlers.Account
{
    public class LoginAdminHandler : IRequestHandler<LoginAdminCommand, AuthResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginAdminHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> Handle(LoginAdminCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.FindUserByEmail(command.Email);
            if (user == null)
            {
                throw new ApplicationException("User Not Found");
            }
            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
            {
                throw new ApplicationException("Password Incorrect");
            }

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email == null ? "" : user.Email),
                new Claim("Name", user.Name == null ? "" : user.Name),
                new Claim("Image", user.Image == null ? "" : user.Image),
                new Claim("IsOtp", user.IsOtp == null ? "" : user.IsOtp.ToString()),
                new Claim("IsSms", user.IsSms == null ? "" : user.IsSms.ToString()),
                new Claim("Role",  user.Role == null ? "" : user.Role.ToString()),
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _unitOfWork.Save(cancellationToken);
            return new AuthResponseDTO()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}