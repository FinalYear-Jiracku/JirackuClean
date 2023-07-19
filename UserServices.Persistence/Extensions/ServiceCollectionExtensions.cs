using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserServices.Application.Interfaces;
using UserServices.Application.Interfaces.IServices;
using UserServices.Infrastructure.Services;
using UserServices.Persistence.Contexts;
using UserServices.Persistence.Repositories;
using UserServices.Persistence.Repositories.Services;
using UserServices.Shared.Middleware;

namespace UserServices.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMappings();
            services.AddDbContext(configuration);
            services.AddRepositories();
        }
        private static void AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
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
                    .AddScoped<ITokenService, TokenServices>()
                    .AddScoped<IUserRepository, UserRepository>();
        }
    }
}
