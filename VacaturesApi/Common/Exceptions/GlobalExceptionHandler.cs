using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace VacaturesApi.Common.Exceptions;

/// <summary>
/// Global Exception Handler used as Program middleware.
/// Handles exceptions thrown in the app and returns them as JSON responses.
/// This way we have a central, extensible mechanism for exception logging and error responses.
/// Uses FluentValidation for validation errors.
/// </summary>

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        // Check if cancellation has been requested
        cancellationToken.ThrowIfCancellationRequested();

        // Determine exception type
        switch (exception)
        {
            case ValidationException validationEx:
                return await HandleValidationException(httpContext, validationEx, cancellationToken);
            case NotFoundException notFoundEx:
                return await HandleNotFoundException(httpContext, notFoundEx, cancellationToken);
            default:
                return await HandleUnhandledException(httpContext, exception, cancellationToken);
        }
    }

    private async Task<bool> HandleValidationException(HttpContext context, ValidationException ex, CancellationToken cancellationToken)
    {
        Log.Error(ex, "Request validation failed");

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var errors = ex.Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key, 
                group => group.Select(e => e.ErrorMessage).ToArray()
            );
        
        var response = new 
        {
            Type = "Validation Error",
            Title = "One or more validation errors occurred",
            Status = StatusCodes.Status400BadRequest,
            Errors = errors
        };
        
        await context.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }

    private async Task<bool> HandleNotFoundException(HttpContext context, NotFoundException ex, CancellationToken cancellationToken)
    {
        Log.Warning(ex, "Resource not found");

        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/json";

        var response = new 
        {
            Type = "Not Found",
            Title = "Resource not found",
            Status = StatusCodes.Status404NotFound,
            Detail = ex.Message
        };

        await context.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }

    private async Task<bool> HandleUnhandledException(HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
        Log.Error(ex, "An unhandled exception occurred");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new 
        {
            Type = "Server Error",
            Title = "An unexpected error occurred",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An internal server error has occurred"
        };

        await context.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}