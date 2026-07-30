using System.Diagnostics;

namespace TaskManagement.API.Middleware
{
    /// <summary>
    /// Middleware for logging incoming HTTP requests and outgoing HTTP responses.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestLoggingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next request delegate in the application pipeline.</param>
        /// <param name="logger">Logger service for recording request and response information.</param>
        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware to log the incoming HTTP request and outgoing HTTP response.
        /// </summary>
        /// <param name="context">The current HTTP context for the request.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Incoming Request: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await _next(context);

            stopwatch.Stop();

            _logger.LogInformation(
                "Outgoing Response: {StatusCode} | Time Taken: {ElapsedMilliseconds} ms",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}