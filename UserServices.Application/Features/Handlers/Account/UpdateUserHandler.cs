using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.Features.Commands.Account;
using UserServices.Application.Interfaces;
using UserServices.Application.Interfaces.IServices;
using UserServices.Domain.Entities;

namespace UserServices.Application.Features.Handlers.Account
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IFirebaseService _firebaseService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.FindUserById(command.Id);
            if (user == null)
            {
                return default;
            }
            if (command.Image == null)
            {
                user.Name = command.Name;
                await _unitOfWork.Repository<User>().UpdateAsync(user);
                user.AddDomainEvent(new UserUpdatedEvent(user));
                await _unitOfWork.Save(cancellationToken);
                return await Task.FromResult(0);
            }

            var document = await _firebaseService.CreateImage(command.Image);
            user.Name = command.Name;
            user.Image = document;
            await _unitOfWork.Repository<User>().UpdateAsync(user);
            user.AddDomainEvent(new UserUpdatedEvent(user));
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(0);
        }
    }
}
