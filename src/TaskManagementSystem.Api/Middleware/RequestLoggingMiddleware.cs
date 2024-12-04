using Microsoft.Extensions.Options;
using TaskManagementSystem.Api.Properties;

namespace TaskManagementSystem.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestLoggingMiddleware> logger;
        private readonly string logText;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger,
            IOptions<RequestLoggingSettings> options)
        {
            this.next = next;
            this.logger = logger;
            this.logText = options.Value.Text;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsCriticalOperation(context))
            {
                logger.LogInformation("{_logText}: {Method} {Path} at {Date} {Time}.",
                    logText,
                    context.Request.Method,
                    context.Request.Path,
                    DateTime.UtcNow.ToString("MM-dd-yyyy"),
                    DateTime.UtcNow.ToString("HH:mm:ss"));
            }
            await next(context);
        }

        private static bool IsCriticalOperation(HttpContext context)
        {
            return context.Request.Method == HttpMethods.Post && (
                context.Request.Path.StartsWithSegments("/users/register") ||
                context.Request.Path.StartsWithSegments("/tasks")
            ) ||
            (context.Request.Method == HttpMethods.Put && context.Request.Path.StartsWithSegments("/tasks")) ||
            (context.Request.Method == HttpMethods.Delete && context.Request.Path.StartsWithSegments("/tasks"));
        }
    }
}
