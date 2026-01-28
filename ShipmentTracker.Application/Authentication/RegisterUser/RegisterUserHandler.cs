using ShipmentTracker.Application.Authentication.Common;
using ShipmentTracker.Application.Services.Authentication;
using ShipmentTracker.Domain.Exceptions;

namespace ShipmentTracker.Application.Authentication.RegisterUser;

public class RegisterUserHandler(IUserAuthenticationService userAuthenticationService, ITokenService tokenService)
{
    public async Task<string> Handle(RegisterUserCommand command)
    {
        if (!CredentialsValidator.IsUsernameAndPasswordValid(command.Email, command.Password))
        {
            throw new ProblemException("Registration Failed", "Email and password are required");
        }

        var createUserResult = await userAuthenticationService.CreateUserAsync(command.Email, command.Password);
        if (!createUserResult.Succeeded)
        {
            throw new ProblemException("Registration Failed", createUserResult.Errors.First());
        }

        var user = await userAuthenticationService.FindUserByEmailAsync(command.Email);
        if (user == null)
        {
            throw new ProblemException("Registration Failed", "Failed to retrieve user after registration");
        }
        
        return tokenService.GenerateJwt(user);
    }
}