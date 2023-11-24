namespace CarCatalogService.Middlewares;

public class AccessDeniedMiddleware
{
    private readonly RequestDelegate _next;

    public AccessDeniedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        if (context.Response.StatusCode >= StatusCodes.Status400BadRequest)
            context.Response.Redirect($"/Errors/{context.Response.StatusCode}");
    }
}
