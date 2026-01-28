using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using ShipmentTracker.Domain.Exceptions;
using ValidationException = ShipmentTracker.Application.Exceptions.ValidationException;

namespace ShipmentTracker.Api.Exceptions;

public class ProblemExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ProblemException problemException)
        {
            return await ProcessUnhandledException(httpContext, exception);
        }
        
        var statusCode = MapToStatusCode(problemException);
        
        var problemDetails = new HttpValidationProblemDetails
        {
            Type = ReasonPhrases.GetReasonPhrase(statusCode),
            Status = statusCode,
            Title = problemException.Error,
            Detail = problemException.Message,
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Errors = validationException.Errors;
        }

        httpContext.Response.StatusCode = statusCode;
        
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
    
    private static int MapToStatusCode(Exception exception) => exception switch
    {
        AuthenticationException => StatusCodes.Status401Unauthorized,
        ValidationException or ProblemException => StatusCodes.Status400BadRequest, 
        _ => StatusCodes.Status500InternalServerError
    };

    private async ValueTask<bool> ProcessUnhandledException(HttpContext httpContext, Exception exception)
    {
        const int statusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.StatusCode = statusCode;
        
        var problemDetails = new ProblemDetails
        {
            Type = ReasonPhrases.GetReasonPhrase(statusCode),
            Status = statusCode,
            Title = "An error occurred",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = statusCode;
    
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}