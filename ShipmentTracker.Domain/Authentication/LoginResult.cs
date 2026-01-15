namespace ShipmentTracker.Domain.Authentication;

// Could make this nicer - perhaps introduce an Error object that has an error type that matches a HTTP status code.
public record LoginResult(bool Success, string? Jwt = null, string? ErrorMessage = null);