using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.Features.Commands.SMS;
using UserServices.Application.Interfaces;

namespace UserServices.Application.Features.Handlers.SMS
{
    public class DisableCodeSmsHandler : IRequestHandler<DisableCodeSmsCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DisableCodeSmsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DisableCodeSmsCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            findUser.IsSms = false;
            return await _unitOfWork.Save(cancellationToken);
        }
    }
}
