namespace ShipmentTracker.Domain.Authentication;

public class User
{
    public required string Id { get; set; }
    public string? Email { get; set; }
    public bool IsAdmin { get; set; }
}