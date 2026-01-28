namespace ShipmentTracker.Application.Authentication.Common;

public record CreateUserResult(bool Succeeded, List<string> Errors);