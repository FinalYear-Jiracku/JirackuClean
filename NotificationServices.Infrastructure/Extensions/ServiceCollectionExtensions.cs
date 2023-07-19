using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NotificationServices.Application.Interfaces.IServices;
using NotificationServices.Domain.Common.Interfaces;
using NotificationServices.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>()
                    .AddScoped<IEmailService, EmailService>()
                    .AddScoped<INotificationEventPulisher, NotificationEventPublisher>()
                    .AddSingleton<IRabbitMQManager, RabbitMQManager>();
        }
    }
}
