using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Application.Authentication.Common;

public interface ITokenService
{
    string GenerateJwt(User user);
}