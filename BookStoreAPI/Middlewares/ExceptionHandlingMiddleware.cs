using Newtonsoft.Json;

namespace BookStoreAPI.Middlewares
{
    /// <summary>
    /// Middleware for handling exceptions during HTTP request processing.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<ExceptionHandlingMiddleware> _logger;
        /// <param name="next">The next component in the request processing pipeline.</param>
        /// <param name="logger">The logger interface for recording exception information.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            // Logging the exception
            _logger.LogError(exception, "An unhandled exception occurred");

            var errorResponse = new ErrorResponse
            {
                Errors = new List<string>
                {
                    "An error occurred while processing the request.",
                    exception.Message // Include the exception message
                }
            };

            var jsonResponse = JsonConvert.SerializeObject(errorResponse);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
    /// <summary>
    /// Use for collecting errors
    /// </summary>
    public class ErrorResponse
    {
        public List<string> Errors { get; set; }
    }
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
