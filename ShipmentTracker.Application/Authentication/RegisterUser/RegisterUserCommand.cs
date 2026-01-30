namespace ShipmentTracker.Application.Authentication.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, string Role);