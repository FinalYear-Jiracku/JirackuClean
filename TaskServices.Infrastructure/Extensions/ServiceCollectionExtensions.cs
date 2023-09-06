using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Common;
using TaskServices.Domain.Common.Interfaces;
using TaskServices.Infrastructure.Services;
using TaskServices.Infrastructure.Services.Publisher;
using TaskServices.Infrastructure.Services.Subcriber;
using TaskServices.Infrastructure.Utils;

namespace TaskServices.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddQuartz();
            services.AddFireBase(configuration);
        }

        private static void AddQuartz(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKeyDeadline = new JobKey("CheckDeadlineJob");
                q.AddJob<CheckDeadlineJob>(opts => opts.WithIdentity(jobKeyDeadline));
                q.AddTrigger(opts => opts
                    .ForJob(jobKeyDeadline)
                    .WithIdentity("CheckDeadlineJob-trigger")
                    .WithSchedule(CronScheduleBuilder
                    .DailyAtHourAndMinute(11,21)
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))) 
                );
                var jobKeyEvent = new JobKey("CheckEventJob");
                q.AddJob<CheckEventJob>(opts => opts.WithIdentity(jobKeyEvent));
                q.AddTrigger(opts => opts
                    .ForJob(jobKeyEvent)
                    .WithIdentity("CheckEventJob-trigger")
                    .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                );
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>()
                    .AddScoped<ICacheService, CacheService>()
                    .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
                    .AddScoped<IFirebaseService, FireBaseService>()
                    .AddScoped<IUserEventSubscriber, UserEventSubscriber>()
                    .AddScoped<INotificationEventSubcriber, NotificationEventSubcriber>()
                    .AddScoped<ICheckDeadlineIssueEventPublisher, CheckDeadlineIssueEventPublisher>()
                    .AddScoped<ICheckDeadlineSubIssueEventPublisher, CheckDeadlineSubIssueEventPublisher>()
                    .AddScoped<ICheckEventPublisher, CheckEventPublisher>()
                    .AddScoped<IPaymentEventPublisher, PaymentEventPublisher>()
                    .AddSingleton<IRabbitMQManager, RabbitMQManager>();
        }
        private static void AddFireBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FirebaseConfiguration>(fireBaseConfig =>
            {
                fireBaseConfig.ApiKey = configuration.GetSection("FireBase:ApiKey").Value;
                fireBaseConfig.Bucket = configuration.GetSection("FireBase:Bucket").Value;
                fireBaseConfig.AuthEmail = configuration.GetSection("FireBase:AuthEmail").Value;
                fireBaseConfig.AuthPassword = configuration.GetSection("FireBase:AuthPassword").Value;
            });
        }
    }
}
