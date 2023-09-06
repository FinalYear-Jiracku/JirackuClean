using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using UserServices.Application.Common.Mappings;
using UserServices.Application.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using UserServices.Application.Utils;
using UserServices.Shared.Pagination.Uris;

namespace UserServices.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddAutoMapper();
            services.AddMediator();
            services.AddServices(configuration);
            services.AddUri();
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
        private static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TwilioConfig>(twilioConfig =>
            {
                twilioConfig.AccountSid = configuration.GetSection("Twilio:AccountSid").Value;
                twilioConfig.AuthToken = configuration.GetSection("Twilio:AuthToken").Value;
                twilioConfig.TwilioPhoneNumber = configuration.GetSection("Twilio:TwilioPhoneNumber").Value;
            });
        }
        private static void AddUri(this IServiceCollection services)
        {
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
        }
    }
}
