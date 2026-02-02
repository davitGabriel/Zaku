using Microsoft.Extensions.DependencyInjection;
using Zaku.Application.Interfaces;
using Zaku.Application.Services;

namespace Zaku.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR and all handlers from this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DI).Assembly));

            // Auth service (not using CQRS per user request)
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
