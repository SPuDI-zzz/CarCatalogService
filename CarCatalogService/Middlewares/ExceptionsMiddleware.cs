using System.Text;

namespace CarCatalogService.Middlewares;

/// <summary>
///     Middleware responsible for handling exceptions that occur during the processing of HTTP requests.
/// </summary>
public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsMiddleware> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExceptionsMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger for capturing exception events.</param>
    public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    ///     Invokes the middleware to catches any unhandled exceptions.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request and response.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An unhandled exception occurred: {ex}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
