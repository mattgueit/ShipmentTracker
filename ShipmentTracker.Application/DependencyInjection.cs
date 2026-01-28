using Microsoft.Extensions.DependencyInjection;
using ShipmentTracker.Application.Authentication;
using ShipmentTracker.Application.Services.Authentication;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services
    )
    {
        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        
        services.AddScoped<LoginHandler>();
        services.AddScoped<RegistrationHandler>();
        
        return services;
    }
}