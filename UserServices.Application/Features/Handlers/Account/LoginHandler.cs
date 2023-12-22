using Google.Apis.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Commands.Account;
using UserServices.Application.Interfaces;
using UserServices.Application.Interfaces.IServices;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Handlers.Account
{
    public partial class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var payload = GoogleJsonWebSignature.ValidateAsync(command.TokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;

            var email = payload.Email;

            var fpt = new Regex("[a-z0-9]+@fpt.edu.vn");
            var fe = new Regex("[a-z0-9]+@fe.edu.vn");

            if (!fpt.IsMatch(email) && !fe.IsMatch(email))
            {
                throw new ApplicationException("Email must be end @fpt.edu.vn or @fe.edu.vn");
            }

            var user = await _unitOfWork.UserRepository.FindUser(payload);


            if (user == null)
            {
                user = new User()
                {
                    Name = payload.Name,
                    Email = payload.Email,
                    Image = payload.Picture,
                    Role = "User",
                    IsDeleted = false,
                    CreatedAt = DateTimeOffset.UtcNow,
                };
                await _unitOfWork.Repository<User>().AddAsync(user);
                user.AddDomainEvent(new UserLoggedInEvent(user));
                await _unitOfWork.Save(cancellationToken);
            }

            if (user.IsDeleted == true)
            {
                throw new ApplicationException("User Already Deleted");
            }

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email == null ? "" : user.Email),
                new Claim("Name", user.Name == null ? "" : user.Name),
                new Claim("Image", user.Image == null ? "" : user.Image),
                new Claim("IsOtp", user.IsOtp.ToString()),
                new Claim("IsSms", user.IsSms.ToString()),
                new Claim("Role", user.Role.ToString()),
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
