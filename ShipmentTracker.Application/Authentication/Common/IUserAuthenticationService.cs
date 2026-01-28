using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Application.Authentication.Common;

public interface IUserAuthenticationService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<CreateUserResult> CreateUserAsync(string email, string password);
    Task<User?> FindUserByEmailAsync(string email);
}