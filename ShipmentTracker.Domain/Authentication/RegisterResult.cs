namespace ShipmentTracker.Domain.Authentication;

// Could make this nicer
public record RegisterResult(bool Success, string? Jwt = null, string? ErrorMessage = null);