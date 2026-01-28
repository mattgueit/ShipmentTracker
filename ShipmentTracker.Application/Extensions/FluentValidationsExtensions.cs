using FluentValidation;
using ValidationException = ShipmentTracker.Application.Exceptions.ValidationException;

namespace ShipmentTracker.Application.Extensions;

public static class FluentValidationsExtensions
{
    public static void ValidateAndThrowValidationException<T>(this IValidator<T> validator, T instance)
    {
        var result = validator.Validate(instance);

        if (!result.IsValid)
        {
            throw new ValidationException(
                "Validation error",
                "One or more validation errors occurred",
                result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        x => x.Key, 
                        x => x.Select(y => y.ErrorMessage).ToArray())
                );
        }
    }
}