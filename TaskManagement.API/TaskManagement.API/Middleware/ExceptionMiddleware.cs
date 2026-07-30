using System.Net;
using TaskManagement.API.DTOModels.Common;

namespace TaskManagement.API.Middleware
{
    /// <summary>
    /// Middleware for globally catching unhandled exceptions and returning standard error responses.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next request delegate in the application pipeline.</param>
        /// <param name="logger">Logger service for logging exception details.</param>
        /// <param name="env">Hosting environment service to check execution environment status.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Invokes the middleware to handle incoming HTTP requests and catch unhandled exceptions.
        /// </summary>
        /// <param name="context">The current HTTP context for the request.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Formats and writes an error response payload to the HTTP response stream.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The unhandled exception encountered.</param>
        /// <returns>A task representing the asynchronous write operation.</returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "An internal server error occurred. Please try again later.",
                Detailed = _env.IsDevelopment() ? exception.Message : null
            };

            return context.Response.WriteAsync(response.ToString());
        }
    }
}