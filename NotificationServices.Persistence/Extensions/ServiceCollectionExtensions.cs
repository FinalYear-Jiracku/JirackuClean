using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationServices.Application.Interfaces;
using NotificationServices.Persistence.Contexts;
using NotificationServices.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(connectionString,
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
                    .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                    .AddScoped<INotificationRepository, NotificationRepository>()
                    .AddScoped<IGroupRepository, GroupRepository>()
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IMessageRepository, MessageRepository>();
        }
    }
}
