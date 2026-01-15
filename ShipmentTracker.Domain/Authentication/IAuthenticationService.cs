namespace ShipmentTracker.Domain.Authentication;

public interface IAuthenticationService
{
    Task<RegisterResult> Register(string email, string password);
    Task<LoginResult> Login(string email, string password);
}