using CarCatalogService.Attributes;
using CarCatalogService.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace CarCatalogService.Filters;

/// <summary>
///     Resource filter that logs information about incoming HTTP requests before action execution.
/// </summary>
public class RequestLogResourceFilter : IAsyncActionFilter
{
    private readonly ILogger<RequestLogResourceFilter> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogResourceFilter"/> class.
    /// </summary>
    /// <param name="logger">The logger for capturing request information.</param>
    public RequestLogResourceFilter(ILogger<RequestLogResourceFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Asynchronously handles action execution and logs request information
    ///     unless the action is marked with the IgnoreLoggingAttribute.
    /// </summary>
    /// <param name="context">The context of the action being executed.</param>
    /// <param name="next">The delegate representing the next action execution in the pipeline.</param>
    /// <returns>A <see cref="Task"/> representing the completion of the action execution.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var hasIgnoreLoggingAttribute = context.ActionDescriptor.EndpointMetadata
            .Any(attribute => attribute is IgnoreLoggingAttribute);

        if (hasIgnoreLoggingAttribute)
        {
            await next();
            return;
        }

        var model = context.ActionArguments
            .FirstOrDefault(keyValuePair => keyValuePair.Value?.GetType()
                .IsSubclassOf(typeof(BaseViewModel)) ?? false)
            .Value;

        var requestInfo = new RequestInfo
        {
            Method = context.HttpContext.Request.Method.ToUpper(),
            Path = context.HttpContext.Request.Path,
            Headers = context.HttpContext.Request.Headers.ToDictionary(header => header.Key, header => header.Value.ToString()),
            Body = model ?? ""
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
