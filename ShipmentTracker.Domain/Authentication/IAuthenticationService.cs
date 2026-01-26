namespace ShipmentTracker.Domain.Authentication;

public interface IAuthenticationService
{
    Task<string> Register(string email, string password);
    Task<string> Login(string email, string password);
}