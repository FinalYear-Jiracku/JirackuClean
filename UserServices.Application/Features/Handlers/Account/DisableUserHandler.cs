using MediatR;
using UserServices.Application.Features.Commands.Account;
using UserServices.Application.Interfaces;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Handlers.Account
{
    public class DisableUserHandler : IRequestHandler<DisableUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DisableUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DisableUserCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.Repository<User>().GetByIdAsync(command.Id);

            if (findUser == null)
            {
                throw new ApplicationException("User Not Found");
            }
            findUser.IsDeleted = true;
            await _unitOfWork.Repository<User>().UpdateAsync(findUser);
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(0);
        }
    }
}
