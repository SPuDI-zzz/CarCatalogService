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
        await _next.Invoke(context);
        var statusCode = context.Response.StatusCode;

        if (IsUnauthorizedStatusCode(statusCode))
        {
            context.Response.Redirect("/Account/Login");
            return;
        }

        if (IsForbiddenStatusCode(statusCode))
        {
            context.Response.Redirect($"/Errors/{StatusCodes.Status404NotFound}");
            return;
        }

        if (IsErrorStatusCode(statusCode))       
            context.Response.Redirect($"/Errors/{statusCode}");
    }

    /// <summary>
    ///     Determines whether the provided HTTP status code indicates an error.
    /// </summary>
    /// <param name="statusCode">The HTTP status code to check.</param>
    /// <returns>
    ///   <c>true</c> if the status code is equal to or greater than <see cref="StatusCodes.Status400BadRequest"/>; otherwise, <c>false</c>.
    /// </returns>
    private bool IsErrorStatusCode(int statusCode) => statusCode >= StatusCodes.Status400BadRequest;

    /// <summary>
    ///     Determines whether the provided HTTP status code indicates an unauthorized access.
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns><c>true</c> if the status code is equal to <see cref="StatusCodes.Status401Unauthorized"/>; otherwise, <c>false</c>.</returns>
    private bool IsUnauthorizedStatusCode(int statusCode) => statusCode == StatusCodes.Status401Unauthorized;

    /// <summary>
    ///     Determines whether the provided HTTP status code indicates an forbidden access.
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns><c>true</c> if the status code is equal to <see cref="StatusCodes.Status403Forbidden"/>; otherwise, <c>false</c>.</returns>
    private bool IsForbiddenStatusCode(int statusCode) => statusCode == StatusCodes.Status403Forbidden;
}
