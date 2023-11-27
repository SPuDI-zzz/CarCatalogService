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
    ///     Invokes the middleware to handle the HTTP request and log any unhandled exceptions.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request and response.</param>
    /// <returns>A <see cref="Task"/> representing the completion of the request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await LogRequest(context);

            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An unhandled exception occurred: {ex}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }

    /// <summary>
    ///     Logs information about the HTTP request, including method, path, headers, and request body.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request and response.</param>
    /// <returns>A <see cref="Task"/> representing the completion of the log operation.</returns>
    private async Task LogRequest(HttpContext context)
    {
        var requestContent = new StringBuilder();

        requestContent.AppendLine("=== Request Info ===");
        requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
        requestContent.AppendLine($"path = {context.Request.Path}");

        requestContent.AppendLine("-- headers");
        foreach (var (headerKey, headerValue) in context.Request.Headers)
        {
            requestContent.AppendLine($"header = {headerKey}    value = {headerValue}");
        }

        requestContent.AppendLine("-- body");
        context.Request.EnableBuffering();
        var requestReader = new StreamReader(context.Request.Body);
        var content = await requestReader.ReadToEndAsync();
        requestContent.AppendLine($"body = {content}");

        _logger.LogInformation(requestContent.ToString());
        context.Request.Body.Position = 0;
    }
}
