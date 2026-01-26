using Microsoft.AspNetCore.Identity;
using ShipmentTracker.Domain.Authentication;
using ShipmentTracker.Domain.Exceptions;

namespace ShipmentTracker.Application.Authentication;

public class RegistrationHandler(UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator)
{
    public async Task<string> Handle(string email, string password)
    {
        if (!CredentialsValidator.IsUsernameAndPasswordValid(email, password))
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
}