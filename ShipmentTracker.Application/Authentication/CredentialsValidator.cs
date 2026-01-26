namespace ShipmentTracker.Application.Authentication;

public static class CredentialsValidator
{
    public static bool IsUsernameAndPasswordValid(string username, string password)
    {
        return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
    }
}