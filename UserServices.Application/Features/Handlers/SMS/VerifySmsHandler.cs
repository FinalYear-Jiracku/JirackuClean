using MediatR;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Commands.SMS;
using UserServices.Application.Interfaces;

namespace UserServices.Application.Features.Handlers.SMS
{
    public class VerifySmsHandler : IRequestHandler<VerifySmsCommand, VerifySmsDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifySmsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VerifySmsDTO> Handle(VerifySmsCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            if (findUser == null)
            {
                throw new ApplicationException("User Not Found");
            }
            TimeSpan timeDifference = (DateTimeOffset.UtcNow - findUser.ExpiredCodeSms).Value.Duration();
            var timeSpan = TimeSpan.FromMinutes(1);
            if (findUser.CodeSmS != command.Code || timeDifference > timeSpan)
            {
                throw new ApplicationException("Code has expired");
            }
            findUser.IsSms = true;
            await _unitOfWork.Save(cancellationToken);
            return new VerifySmsDTO()
            {
                Status = true
            };

        }
    }
}
