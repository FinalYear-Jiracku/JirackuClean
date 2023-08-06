using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.Interfaces.IServices;
using UserServices.Domain.Common;
using UserServices.Domain.Common.Interfaces;
using UserServices.Infrastructure.Services;

namespace UserServices.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddStripe(configuration);
        }

        private static void AddStripe(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetConnectionString("Stripe:SecretKey");
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>()
                    .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
                    .AddScoped<IFirebaseService, FirebaseService>()
                    .AddScoped<IUserEventPublisher, UserEventPublisher>()
                    .AddScoped<CustomerService>()
                    .AddScoped<ChargeService>()
                    .AddScoped<TokenService>()
                    .AddSingleton<IRabbitMQManager, RabbitMQManager>();
        }
    }
}
