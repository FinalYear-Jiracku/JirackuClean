using MediatR;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using UserServices.Application.Features.Commands;
using UserServices.Application.Interfaces;
using UserServices.Application.Utils;

namespace UserServices.Application.Features.Handlers
{
    public class SendSmsCodeHandler : IRequestHandler<SendSmsCodeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TwilioConfig _twilioConfig;

        public SendSmsCodeHandler(IUnitOfWork unitOfWork, IOptions<TwilioConfig> twilioConfig)
        {
            _unitOfWork = unitOfWork;
            _twilioConfig = twilioConfig.Value;
        }

        public async Task<int> Handle(SendSmsCodeCommand command, CancellationToken cancellationToken)
        {
            var findUser = await _unitOfWork.UserRepository.FindUserById(command.UserId);
            if (findUser == null)
            {
                throw new ApplicationException("User Not Found");
            }
            var otpCode = GenerateRandomOTP();
            
            TwilioClient.Init(_twilioConfig.AccountSid, _twilioConfig.AuthToken);
            MessageResource.Create(
                body: $"Your OTP code is: {otpCode} and exprired after 1 minute",
                from: new Twilio.Types.PhoneNumber(_twilioConfig.TwilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber($"whatsapp:{command.Phone}")
            );
            findUser.CodeSmS = otpCode;
            findUser.Phone = command.Phone;
            findUser.ExpiredCodeSms = DateTimeOffset.UtcNow.AddMinutes(1);
            await _unitOfWork.Save(cancellationToken);
            return await Task.FromResult(0);
        }
        public static string GenerateRandomOTP()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }
    }
}
