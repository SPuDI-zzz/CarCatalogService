namespace CarCatalogService.Middlewares;

/// <summary>
///     Middleware responsible for redirecting to error pages for HTTP status codes indicating errors.
/// </summary>
public class ErrorRedirectorMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ErrorRedirectorMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger for capturing error redirection events.</param>
    public ErrorRedirectorMiddleware(RequestDelegate next)
    {
        _next = next;
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
        }
    }

    /// <summary>
    ///     Determines whether the provided HTTP status code indicates an error.
    /// </summary>
    /// <param name="statusCode">The HTTP status code to check.</param>
    /// <returns>
    ///   <c>true</c> if the status code is equal to or greater than <see cref="StatusCodes.Status400BadRequest"/>; otherwise, <c>false</c>.
    /// </returns>
    private bool IsErrorStatusCode(int statusCode) => statusCode >= StatusCodes.Status400BadRequest;
}
