namespace ShipmentTracker.Application.Services.Authentication;

public static class CredentialsValidator
{
    public static bool IsUsernameAndPasswordValid(string username, string password)
    {
        return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
    }
}