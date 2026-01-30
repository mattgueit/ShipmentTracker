using FluentValidation;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Application.Authentication.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");

        RuleFor(x => x.Role)
            .Must(x => Roles.AllRoles.Contains(x))
            .WithMessage($"Invalid role. Must be one of {string.Join(", ", Roles.AllRoles)}");
    }
}