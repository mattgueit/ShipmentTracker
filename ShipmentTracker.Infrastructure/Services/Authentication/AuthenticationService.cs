using Microsoft.AspNetCore.Identity;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Infrastructure.Services.Authentication;

public class AuthenticationService(UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator) : IAuthenticationService
{
    public async Task<RegisterResult> Register(string email, string password)
    {
        if (!IsUsernameAndPasswordValid(email, password))
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

    public async Task<LoginResult> Login(string email, string password)
    {
        if (!IsUsernameAndPasswordValid(email, password))
        {
            return new LoginResult(false, null, "Email and password are required");
        }

        var user = await userManager.FindByEmailAsync(email);

        if (user == null || !await userManager.CheckPasswordAsync(user, password))
        {
            return new LoginResult(false, null, "Unauthorized");
        }
        
        var jwt = jwtGenerator.GenerateJwt(user);
        return new LoginResult(true, jwt);
    }

    private static bool IsUsernameAndPasswordValid(string username, string password)
    {
        return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
    }
}