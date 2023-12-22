using Google.Apis.Auth;
using MediatR;
using OtpNet;
using Stripe.Apps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Commands.QRCode;
using UserServices.Application.Interfaces;
using UserServices.Application.Interfaces.IServices;

namespace UserServices.Application.Features.Handlers.QRCode
{
    public class GenerateQRCodeHandler : IRequestHandler<GenerateQRCodeCommand, QRCodeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateQRCodeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<QRCodeDTO> Handle(GenerateQRCodeCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            var secret = KeyGeneration.GenerateRandomKey(20);
            var base32Secret = Base32Encoding.ToString(secret);

            findUser.SecretKey = base32Secret;
            await _unitOfWork.Save(cancellationToken);

            var uriString = new OtpUri(OtpType.Totp, base32Secret, command.Email, "Jiracku").ToString();

            return new QRCodeDTO()
            {
                Secret = base32Secret,
                QrCodeUrl = uriString,
            };
        }
    }
}
