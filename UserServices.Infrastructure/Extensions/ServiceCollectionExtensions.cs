using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using UserServices.Application.Interfaces.IServices;
using UserServices.Application.Utils;
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
            services.AddFireBase(configuration);
        }

        private static void AddStripe(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetConnectionString("Stripe:SecretKey");
        }

        private static void AddFireBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FirebaseConfiguration>(firebaseConfig =>
            {
                firebaseConfig.ApiKey = configuration.GetSection("FireBase:ApiKey").Value;
                firebaseConfig.Bucket = configuration.GetSection("FireBase:Bucket").Value;
                firebaseConfig.AuthEmail = configuration.GetSection("FireBase:AuthEmail").Value;
                firebaseConfig.AuthPassword = configuration.GetSection("FireBase:AuthPassword").Value;
            });
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>()
                    .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
                    .AddScoped<IFirebaseService, FirebaseService>()
                    .AddScoped<CustomerService>()
                    .AddScoped<ChargeService>()
                    .AddScoped<TokenService>()
                    .AddScoped<IUserEventPublisher, UserEventPublisher>()
                    .AddSingleton<IRabbitMQManager, RabbitMQManager>();
        }
    }
}
