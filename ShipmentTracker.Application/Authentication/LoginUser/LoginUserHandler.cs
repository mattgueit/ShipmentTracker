using FluentValidation;
using ShipmentTracker.Application.Authentication.Common;
using ShipmentTracker.Application.Extensions;
using ShipmentTracker.Domain.Exceptions;

namespace ShipmentTracker.Application.Authentication.LoginUser;

public class LoginUserHandler(
    IValidator<LoginUserCommand> validator,
    IUserAuthenticationService userAuthenticationService,
    ITokenService tokenService
)
{
    public async Task<string> Handle(LoginUserCommand command)
    {
        validator.ValidateAndThrowValidationException(command);
        
        var user = await userAuthenticationService.AuthenticateAsync(command.Email, command.Password);

        if (user == null)
        {
            throw new AuthenticationException("Authentication Failed", "Invalid username or password");
        }
        
        return tokenService.GenerateJwt(user);
    }
}