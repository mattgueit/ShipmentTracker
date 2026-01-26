using Microsoft.AspNetCore.Identity;
using ShipmentTracker.Domain.Authentication;
using ShipmentTracker.Domain.Exceptions;

namespace ShipmentTracker.Infrastructure.Services.Authentication;

public class AuthenticationService(UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator) : IAuthenticationService
{
    public async Task<string> Register(string email, string password)
    {
        if (!IsUsernameAndPasswordValid(email, password))
        {
            throw new ProblemException("Registration Failed", "Email and password are required");
        }

        var identityResult = await userManager.CreateAsync(new ApplicationUser { UserName = email, Email = email }, password);
        if (!identityResult.Succeeded)
        {
            throw new ProblemException("Registration Failed", identityResult.Errors.First().Description);
        }

        var user = await userManager.FindByNameAsync(email);
        if (user == null)
        {
            throw new ProblemException("Registration Failed", "Failed to retrieve user after registration");
        }
        
        return jwtGenerator.GenerateJwt(user);
    }

    public async Task<string> Login(string email, string password)
    {
        if (!IsUsernameAndPasswordValid(email, password))
        {
            throw new ProblemException("Login Failed", "Email and password are required");
        }

        var user = await userManager.FindByEmailAsync(email);

        if (user == null || !await userManager.CheckPasswordAsync(user, password))
        {
            throw new AuthenticationException("Authentication Failed", "Invalid username or password");
        }
        
        return jwtGenerator.GenerateJwt(user);
    }

    private static bool IsUsernameAndPasswordValid(string username, string password)
    {
        return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
    }
}