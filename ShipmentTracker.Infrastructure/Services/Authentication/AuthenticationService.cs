using Microsoft.AspNetCore.Identity;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Infrastructure.Services.Authentication;

public class AuthenticationService(UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator) : IAuthenticationService
{
    public async Task<RegisterResult> Register(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return new RegisterResult( false, null, "Email and password are required");
        }

        var identityResult = await userManager.CreateAsync(new ApplicationUser { UserName = email, Email = email }, password);
        if (!identityResult.Succeeded)
        {
            return new RegisterResult(false, null, identityResult.Errors.First().Description);
        }

        var user = await userManager.FindByNameAsync(email);
        if (user == null)
        {
            return new RegisterResult(false, null, "Failed to retrieve user after registration");
        }
        
        var jwt = jwtGenerator.GenerateJwt(user);
        return new RegisterResult(true, jwt);
    }
}