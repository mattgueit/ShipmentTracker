namespace ShipmentTracker.Domain.Exceptions;

public class AuthenticationException(string error, string message) : ProblemException(error, message)
{
    
}