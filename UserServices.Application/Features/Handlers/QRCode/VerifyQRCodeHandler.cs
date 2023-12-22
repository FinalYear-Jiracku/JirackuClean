using MediatR;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Commands.QRCode;
using UserServices.Application.Interfaces;
using static System.Net.WebRequestMethods;

namespace UserServices.Application.Features.Handlers.QRCode
{
    public class VerifyQRCodeHandler : IRequestHandler<VerifyQRCodeCommand, VerifyQRCodeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyQRCodeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VerifyQRCodeDTO> Handle(VerifyQRCodeCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            var totp = new Totp(Base32Encoding.ToBytes(findUser.SecretKey));
            bool isOTPValid = totp.VerifyTotp(command.Code, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);
            if (isOTPValid)
            {
                findUser.IsOtp = true;
                await _unitOfWork.Save(cancellationToken);
                return new VerifyQRCodeDTO()
                {
                    Status = true
                };
            }
            else
            {
                return new VerifyQRCodeDTO()
                {
                    Status = false
                };
            }
        }
    }
}
