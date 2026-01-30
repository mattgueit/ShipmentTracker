using FluentValidation;
using ShipmentTracker.Application.Authentication.Common;
using ShipmentTracker.Application.Extensions;
using ShipmentTracker.Domain.Exceptions;

namespace ShipmentTracker.Application.Authentication.RegisterUser;

public class RegisterUserHandler(
    IValidator<RegisterUserCommand> validator,
    IUserAuthenticationService userAuthenticationService,
    IRoleService roleService,
    ITokenService tokenService
)
{
    public async Task<string> Handle(RegisterUserCommand command)
    {
        validator.ValidateAndThrowValidationException(command);

        if (!await roleService.RoleExistsAsync(command.Role))
        {
            throw new ProblemException("Registration Failed", "Role does not exist");
        }
        
        var createUserResult = await userAuthenticationService.CreateUserAsync(command.Email, command.Password);
        if (!createUserResult.Succeeded)
        {
            throw new ProblemException("Registration Failed", createUserResult.Errors.First());
        }
        
        var addUserToRoleResult = await userAuthenticationService.AddUserToRoleAsync(command.Email, command.Role);
        if (!addUserToRoleResult.Succeeded)
        {
            throw new ProblemException("Registration Failed", addUserToRoleResult.Errors.First());
        }

        var user = await userAuthenticationService.FindUserByEmailAsync(command.Email);
        if (user == null)
        {
            throw new ProblemException("Registration Failed", "Failed to retrieve user after registration");
        }
        
        return tokenService.GenerateJwt(user);
    }
}