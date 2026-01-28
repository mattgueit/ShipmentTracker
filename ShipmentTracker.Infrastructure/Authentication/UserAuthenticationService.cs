using Microsoft.AspNetCore.Identity;
using ShipmentTracker.Application.Authentication.Common;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Infrastructure.Authentication;

public class UserAuthenticationService(UserManager<ApplicationUser> userManager) : IUserAuthenticationService
{
    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null) return null;

        if (await userManager.CheckPasswordAsync(user, password))
        {
            return new User
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        return null;
    }
    
    public async Task<CreateUserResult> CreateUserAsync(string email, string password)
    {
        var identityResult = await userManager.CreateAsync(new ApplicationUser { UserName = email, Email = email }, password);
        
        return new CreateUserResult(identityResult.Succeeded, identityResult.Errors.Select(x => x.Description).ToList());
    }

    public async Task<User?> FindUserByEmailAsync(string email)
    {
        var user = await userManager.FindByNameAsync(email);

        if (user == null) return null;

        return new User
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}