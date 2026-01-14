using Microsoft.Extensions.DependencyInjection;
using ShipmentTracker.Domain.Authentication;
using ShipmentTracker.Service.Authentication;

namespace ShipmentTracker.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        
        return services;
    }
}