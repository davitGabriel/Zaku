using System.Net;
using System.Text.Json;

namespace Zaku.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred");

            var (statusCode, message) = exception switch
            {
                InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, exception.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, exception.Message),
                ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new { message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
