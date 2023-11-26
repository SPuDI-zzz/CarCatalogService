namespace CarCatalogService.Middlewares;

/// <summary>
///     Middleware responsible for redirecting to error pages for HTTP status codes indicating errors.
/// </summary>
public class ErrorRedirectorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorRedirectorMiddleware> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ErrorRedirectorMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger for capturing error redirection events.</param>
    public ErrorRedirectorMiddleware(RequestDelegate next, ILogger<ErrorRedirectorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    ///     Invokes the middleware to handle the HTTP request.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request and response.</param>
    /// <returns>A <see cref="Task"/> representing the completion of the request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        var statusCode = context.Response.StatusCode;
        if (IsErrorStatusCode(statusCode))
        {
            context.Response.Redirect($"/Errors/{statusCode}");
            _logger.LogInformation($"Status Code: {statusCode}", context.Response.StatusCode);
        }
    }

    private bool IsErrorStatusCode(int statusCode) => statusCode >= StatusCodes.Status400BadRequest;
}
