using System.Text.Json;
using TimeTrackingApp.Results;
using Microsoft.AspNetCore.Diagnostics;

namespace TimeTrackingApp.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        ValidationError error = new ValidationError("Internal Server Error", "An unexpected error occurred");
        List<ValidationError> errors = new List<ValidationError>() { error };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
        string jsonResponse = JsonSerializer.Serialize(errors);
            
        context.Response.ContentType = "application/json";
            
        await context.Response.WriteAsync(jsonResponse);

        return true;
    }
}
