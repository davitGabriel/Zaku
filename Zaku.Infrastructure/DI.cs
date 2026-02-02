using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// Authentication setup is configured in the API project. This library only binds JwtSettings and registers the token generator.
using System;
using System.Collections.Generic;
using System.Text;
using Zaku.Infrastructure.Data;
using Zaku.Infrastructure.Repositories;
using Zaku.Domain.Interfaces;
using Zaku.Infrastructure.Security;
using Zaku.Domain.Entities;

namespace Zaku.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // DB Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Register repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Register generic repository so concrete repositories (e.g. OrderRepository) can be resolved
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Register JWT token generator
            services.AddSingleton<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
