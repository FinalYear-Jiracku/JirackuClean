using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NotificationServices.Application.Interfaces.IServices;
using NotificationServices.Domain.Common;
using NotificationServices.Domain.Common.Interfaces;
using NotificationServices.Infrastructure.Services;
using NotificationServices.Infrastructure.Services.Publisher;
using NotificationServices.Infrastructure.Services.Subcriber;

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
                    .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
                    .AddTransient<IEmailService, EmailService>()
                    .AddTransient<INotificationEventPulisher, NotificationEventPublisher>()
                    .AddHostedService<CheckDeadlineSubIssueEventSubcriber>()
                    .AddHostedService<CheckDeadlineIssueEventSubcriber>()
                    .AddSingleton<IRabbitMQManager, RabbitMQManager>();
        }
    }
}
