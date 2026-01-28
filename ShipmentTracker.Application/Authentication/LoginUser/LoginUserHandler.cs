using ShipmentTracker.Application.Authentication.Common;
using ShipmentTracker.Application.Services.Authentication;
using ShipmentTracker.Domain.Exceptions;

namespace ShipmentTracker.Application.Authentication.LoginUser;

public class LoginUserHandler(IUserAuthenticationService userAuthenticationService, ITokenService tokenService)
{
    public async Task<string> Handle(LoginUserCommand command)
    {
        if (!CredentialsValidator.IsUsernameAndPasswordValid(command.Email, command.Password))
        {
            throw new ProblemException("Login Failed", "Email and password are required");
        }

        var user = await userAuthenticationService.AuthenticateAsync(command.Email, command.Password); 

        if (user == null)
        {
            throw new AuthenticationException("Authentication Failed", "Invalid username or password");
        }
        
        return tokenService.GenerateJwt(user);
    }
}