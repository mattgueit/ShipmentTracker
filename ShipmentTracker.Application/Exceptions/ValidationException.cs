using ShipmentTracker.Domain.Exceptions;

namespace ShipmentTracker.Application.Exceptions;

public class ValidationException(string error, string message, Dictionary<string, string[]> errors) : ProblemException(error, message)
{
    public Dictionary<string, string[]> Errors { get; } = errors;
}