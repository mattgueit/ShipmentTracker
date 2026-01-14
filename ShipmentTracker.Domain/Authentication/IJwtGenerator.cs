namespace ShipmentTracker.Domain.Authentication;

public interface IJwtGenerator
{
    string GenerateJwt(ApplicationUser user);
}