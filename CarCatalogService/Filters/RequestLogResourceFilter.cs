using CarCatalogService.Attributes;
using CarCatalogService.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection.PortableExecutable;
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

        var method = context.HttpContext.Request.Method.ToUpper();

        var path = context.HttpContext.Request.Path;

        var headers = context.HttpContext.Request.Headers.ToDictionary(header => header.Key, header => header.Value.ToString());

        var body = context.ActionArguments
            .FirstOrDefault(keyValuePair => keyValuePair.Value?.GetType()
                .IsSubclassOf(typeof(BaseViewModel)) ?? false)
            .Value ?? string.Empty;

        _logger.LogInformation("{@Method}, {@Path}, {@Headers}, @{Body}", method, path, headers, body);

        await next();
    }
}
