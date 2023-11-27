using System.Text.Json;

namespace CarCatalogService.Middlewares;

/// <summary>
///     Middleware responsible for logging information about incoming HTTP requests.
/// </summary>
public class RequestLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggerMiddleware> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLoggerMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger for capturing request information.</param>
    public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    ///     Invokes the middleware to handle the incoming HTTP request, logs the request information, and passes to the next middleware.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request and response.</param>
    /// <returns>A <see cref="Task"/> representing the completion of the request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        await LogRequest(context);

        await _next.Invoke(context);
    }

    /// <summary>
    ///     Logs information about the incoming HTTP request.
    /// </summary>
    /// <param name="context">The HttpContext representing the current request and response.</param>
    /// <returns>A <see cref="Task"/> representing the completion of the log operation.</returns>
    private async Task LogRequest(HttpContext context)
    {

        RequestInfo requestInfo;
        context.Request.EnableBuffering();
        var reader = new StreamReader(context.Request.Body);
        
        var body = await reader.ReadToEndAsync();
        requestInfo = new RequestInfo
        {
            Method = context.Request.Method.ToUpper(),
            Path = context.Request.Path,
            Headers = context.Request.Headers.ToDictionary(header => header.Key, header => header.Value.ToString()),
            Body = body
        };
        context.Request.Body.Position = 0;
        

        var requestJson = JsonSerializer.Serialize(requestInfo);
        _logger.LogInformation($"{requestJson}");
    }

    /// <summary>
    ///     Represents information about an HTTP request for logging purposes.
    /// </summary>
    private class RequestInfo
    {
        /// <summary>
        ///     Gets or sets the HTTP method used in the request.
        /// </summary>
        public string? Method { get; set; }

        /// <summary>
        ///     Gets or sets the path of the request.
        /// </summary>
        public PathString Path { get; set; }

        /// <summary>
        ///     Gets or sets the headers of the request.
        /// </summary>
        public Dictionary<string, string>? Headers { get; set; }

        /// <summary>
        ///     Gets or sets the body content of the request.
        /// </summary>
        public string? Body { get; set; }
    }
}
