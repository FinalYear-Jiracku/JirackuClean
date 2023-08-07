using Google.Apis.Auth;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.DTOs;
using UserServices.Application.Features.Commands;
using UserServices.Application.Interfaces;
using UserServices.Application.Interfaces.IServices;

namespace UserServices.Application.Features.Handlers
{
    public class PaymentIntentHandler : IRequestHandler<PaymentIntentCommand, PaymentIntentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public PaymentIntentHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<PaymentIntentDTO> Handle(PaymentIntentCommand command, CancellationToken cancellationToken)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = 50000,
                Currency = "vnd",
                PaymentMethodTypes = new List<string> { "card" }
            });
            return new PaymentIntentDTO()
            {
                ClientSecret = paymentIntent.ClientSecret
            };
        }
    }
}
