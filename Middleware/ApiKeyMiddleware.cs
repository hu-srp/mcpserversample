using McpServerSample.Config;
using Serilog;

namespace McpServerSample.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
            var providedKey = ExtractBearerToken(authHeader);

            if (string.IsNullOrEmpty(providedKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Missing API key");
                return;
            }

            if (!ApiKeyManager.IsValidApiKey(providedKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid API key");
                return;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error during authentication for request {Path}", context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Internal server error");
        }
    }

    private static string? ExtractBearerToken(string? authHeader)
    {
        if (string.IsNullOrEmpty(authHeader))
            return null;

        if (!authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return null;

        return authHeader["Bearer ".Length..].Trim();
    }
}

public static class ApiKeyMiddlewareExtensions
{
    public static IApplicationBuilder UseApiKeyAuthentication(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyMiddleware>();
    }
}
