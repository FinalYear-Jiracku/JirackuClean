using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Quartz;
using Quartz.Impl.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Common;
using TaskServices.Domain.Common.Interfaces;
using TaskServices.Infrastructure.Services;
using TaskServices.Infrastructure.Services.Publisher;
using TaskServices.Infrastructure.Services.Subcriber;

namespace TaskServices.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
            services.AddQuartz();
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
                    .WithSchedule(CronScheduleBuilder
                    .DailyAtHourAndMinute(16,50)
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")))
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
    }
}
