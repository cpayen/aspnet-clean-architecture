using System.Diagnostics;
using System.Net;

namespace WebApp.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;
    
    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var sw = Stopwatch.StartNew();

        try
        {
            await _next(context);

            sw.Stop();
            var elapsed = sw.Elapsed.TotalMilliseconds;

            HandleLogging(context, elapsed, _logger);
        }
        catch (Exception exception)
        {
            sw.Stop();
            var elapsed = sw.Elapsed.TotalMilliseconds;

            HandleLogging(context, elapsed, _logger, exception);
        }
    }

    private static void HandleLogging(HttpContext context, double elapsed, ILogger logger, Exception? exception = null)
    {
        var code = context.Response.StatusCode;
        var method = context.Request.Method;
        var path = context.Request.Path;

        var log = $"{code} - HTTP {method} {path} in {elapsed:0.0000} ms";
        if (exception != null)
        {
            log = $"{log}{Environment.NewLine}{exception}";
        }

        switch (code)
        {
            case (int)HttpStatusCode.OK:
            case (int)HttpStatusCode.Created:
            case (int)HttpStatusCode.NoContent:
                logger.LogInformation("{Log}", log);
                break;

            case (int)HttpStatusCode.InternalServerError:
                
                logger.LogCritical("{Log}", log);
                break;

            case (int)HttpStatusCode.NotFound:
                logger.LogError("{Log}", log);
                break;

            default:
                logger.LogError("{Log}", log);
                break;
        }
    }
}