using NotificationServiceApi.DTOs;

namespace NotificationServiceApi.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiKeyMiddleware> _logger;
    private const string ApiKeyHeaderName = "X-API-KEY";

    public ApiKeyMiddleware(
        RequestDelegate next,
        IConfiguration configuration,
        ILogger<ApiKeyMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower() ?? "";
        if (path == "/" || 
            path.Contains("/swagger") || 
            path.Contains("/index.html"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            _logger.LogWarning("Missing API Key in request headers.");
            await ReturnUnauthorized(context, "API Key was not provided.");
            return;
        }

        var apiKey = _configuration.GetValue<string>("Authentication:ApiKey");

        if (apiKey == null || !apiKey.Equals(extractedApiKey))
        {
            _logger.LogWarning("Invalid API Key provided.");
            await ReturnUnauthorized(context, "Unauthorized client.");
            return;
        }

        await _next(context);
    }

    private static async Task ReturnUnauthorized(HttpContext context, string message)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";

        var response = ApiResponse<object>.ErrorResponse(message, statusCode: StatusCodes.Status401Unauthorized);
        await context.Response.WriteAsJsonAsync(response);
    }
}

public static class ApiKeyMiddlewareExtensions
{
    public static IApplicationBuilder UseApiKey(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyMiddleware>();
    }
}
