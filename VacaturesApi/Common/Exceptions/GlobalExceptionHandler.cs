using FluentValidation;
using Serilog;
using VacaturesApi.Common.Exceptions;

public class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Log.Error("_______________USED GLOBAL HANDLER_________________");
        try
        {
            // Continue processing the request
            await next(context);
        }
        catch (ValidationException ex)
        {
            Log.Error(ex, "Validation failed");

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
            
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (NotFoundException ex)
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

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
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

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}