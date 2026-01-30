namespace ShipmentTracker.Application.Authentication.Common;

public record AddUserToRoleResult(bool Succeeded, List<string> Errors);