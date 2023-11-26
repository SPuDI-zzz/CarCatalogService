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
            var originalResponseBody = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next.Invoke(context);

            await LogResponse(context, responseBody, originalResponseBody);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An unhandled exception occurred: {ex}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }

    /// <summary>
    ///     Logs information about the HTTP response, including status code, headers, and response body.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request and response.</param>
    /// <param name="responseBody">The MemoryStream containing the captured response content.</param>
    /// <param name="originalResponseBody">The original response body.</param>
    /// <returns>A <see cref="Task"/> representing the completion of the log operation.</returns>
    private async Task LogResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody)
    {
        var responseContent = new StringBuilder();
        responseContent.AppendLine("=== Response Info ===");

        responseContent.AppendLine($"Status Code = {context.Response.StatusCode}");

        responseContent.AppendLine("-- headers");
        foreach (var (headerKey, headerValue) in context.Response.Headers)
        {
            responseContent.AppendLine($"header = {headerKey}    value = {headerValue}");
        }
        
        responseContent.AppendLine("-- body");
        responseBody.Position = 0;
        var content = await new StreamReader(responseBody).ReadToEndAsync();
        responseContent.AppendLine($"body = {content}");
        responseBody.Position = 0;
        await responseBody.CopyToAsync(originalResponseBody);
        context.Response.Body = originalResponseBody;

        _logger.LogInformation(responseContent.ToString());
    }
}
