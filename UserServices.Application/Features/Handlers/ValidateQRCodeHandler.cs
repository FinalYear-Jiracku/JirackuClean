using MediatR;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Commands;
using UserServices.Application.Interfaces;

namespace UserServices.Application.Features.Handlers
{
    public class ValidateQRCodeHandler : IRequestHandler<ValidateQRCodeCommand, ValidateQRCodeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateQRCodeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidateQRCodeDTO> Handle(ValidateQRCodeCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            var totp = new Totp(Base32Encoding.ToBytes(findUser.SecretKey), step: 30);
            bool isOTPValid = totp.VerifyTotp(command.Code, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);
            if (isOTPValid)
            {
                return new ValidateQRCodeDTO()
                {
                    Status = true
                };
            }
            else
            {
                return new ValidateQRCodeDTO()
                {
                    Status = false
                };
            }
        }
    }
}
