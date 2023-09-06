using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.Features.Commands;
using UserServices.Application.Interfaces;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Handlers
{
    public class EnableUserHandler : IRequestHandler<EnableUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnableUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(EnableUserCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.Repository<User>().GetByIdAsync(command.Id);

            if (findUser == null)
            {
                throw new ApplicationException("User Not Found");
            }
            findUser.IsDeleted = false;
            await _unitOfWork.Repository<User>().UpdateAsync(findUser);
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(0);
        }
    }
}
