﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Common.Mappings;
using TaskServices.Application.Interfaces;
using TaskServices.Persistence.Contexts;
using TaskServices.Persistence.Repositories;
using TaskServices.Persistence.Services;
using TaskServices.Shared.Middleware;

namespace TaskServices.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddAutoMapper();
            services.AddRepositories();
        }

        public static void PersistenceConfigure(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //var configuration = new MapperConfiguration(cfg => {
            //    cfg.AddProfile<MappingProfile>();
            //});
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
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
                    .AddScoped<IProjectRepository, ProjectRepository>()
                    .AddScoped<ICacheService, CacheService>();
        }
    }
}
