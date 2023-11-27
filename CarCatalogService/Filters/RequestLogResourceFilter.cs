using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace CarCatalogService.Filters;

public class RequestLogResourceFilter : Attribute, IAsyncActionFilter
{
    private readonly ILogger<RequestLogResourceFilter> _logger;

    public RequestLogResourceFilter(ILogger<RequestLogResourceFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var entity = context.ActionArguments.FirstOrDefault(key => key.Key.Contains("Model")).Value ?? "";

        var requestInfo = new RequestInfo
        {
            Method = context.HttpContext.Request.Method.ToUpper(),
            Path = context.HttpContext.Request.Path,
            Headers = context.HttpContext.Request.Headers.ToDictionary(header => header.Key, header => header.Value.ToString()),
            Body = entity
        };
        var requestJson = JsonSerializer.Serialize(requestInfo);
        _logger.LogInformation($"{requestJson}");

        await next();
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
        public object? Body { get; set; }
    }
}
