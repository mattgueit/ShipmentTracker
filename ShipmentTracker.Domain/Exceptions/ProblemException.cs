namespace ShipmentTracker.Domain.Exceptions;

public class ProblemException(string error, string message) : Exception
{
    public string Error { get; } = error;
    public override string Message { get; } = message;
}