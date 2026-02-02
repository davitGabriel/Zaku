using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Zaku.Application.Interfaces;
using Zaku.Application.Services;

namespace Zaku.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
