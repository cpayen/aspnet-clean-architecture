using System.Net;
using System.Text.Json;
using Application.Exceptions;

namespace WebApp.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);

            // Throw the exception for other middlewares
            throw;
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // 500 if unexpected
        var code = HttpStatusCode.InternalServerError;
        var title = "Server error";

        switch (exception)
        {
            // 404 not found
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                title = "Not found";
                break;
            
            // 400 bad request
            case ValidationException:
                code = HttpStatusCode.BadRequest;
                title = "Bad request";
                break;
        }

        var result = JsonSerializer.Serialize(new { title, status = (int)code, error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        await context.Response.WriteAsync(result);
    }
}