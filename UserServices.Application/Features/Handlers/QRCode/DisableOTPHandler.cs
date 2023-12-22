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

namespace UserServices.Application.Features.Handlers.QRCode
{
    public class DisableOTPHandler : IRequestHandler<DisableOTPCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DisableOTPHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DisableOTPCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            findUser.IsOtp = false;
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
