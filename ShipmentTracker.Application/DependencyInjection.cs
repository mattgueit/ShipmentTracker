using Microsoft.Extensions.DependencyInjection;
using ShipmentTracker.Application.Authentication.LoginUser;
using ShipmentTracker.Application.Authentication.RegisterUser;

namespace ShipmentTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services
    )
    {
        services.AddScoped<LoginUserHandler>();
        services.AddScoped<RegisterUserHandler>();
        
        return services;
    }
}