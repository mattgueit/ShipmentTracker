using Microsoft.AspNetCore.Identity;
using ShipmentTracker.Domain.Authentication;
using ShipmentTracker.Domain.Exceptions;

namespace ShipmentTracker.Application.Authentication;

public class LoginHandler(UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator)
{
    public async Task<string> Handle(string email, string password)
    {
        if (!CredentialsValidator.IsUsernameAndPasswordValid(email, password))
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
}