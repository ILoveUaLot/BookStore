using Azure.Core;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Middlewares
{
    /// <summary>
    /// Middleware for logging orders made
    /// </summary>
    public class OrderLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<OrderLoggerMiddleware> _logger;
        public OrderLoggerMiddleware(RequestDelegate next, ILogger<OrderLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // The InvokeAsync method is responsible for handling the incoming HTTP request
        public async Task InvokeAsync(HttpContext httpContext)
        {
            // Check if the incoming request is a POST request to the "/api/order" endpoint
            if (httpContext.Request.Path.StartsWithSegments("/api/order", StringComparison.OrdinalIgnoreCase) &&
               httpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                // Backup the original stream
                var originalBodyStream = httpContext.Request.Body;

                try
                {
                    using (var requestBodyStream = new MemoryStream())
                    {
                        await httpContext.Request.Body.CopyToAsync(requestBodyStream);
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        var requestBody = new StreamReader(requestBodyStream).ReadToEnd();
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        // Replace the original stream
                        httpContext.Request.Body = requestBodyStream;

                        var order = JsonConvert.DeserializeObject<OrderModel>(requestBody);

                        _logger.LogInformation($"Made order: {order.Id}; " +
                            $"Order accepted for processing {order.OrderDate}");
                        foreach (var item in order.Books)
                        {
                            _logger.LogInformation($"{item.Title} was ordered; it is id {item.Id}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error reading request body");
                }
                finally
                {
                    // Restore the original stream
                    httpContext.Request.Body = originalBodyStream;
                }
            }

            // Call the next middleware in the pipeline
            await _next(httpContext);
        }    
    }

    public static class OrderLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseOrderLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OrderLoggerMiddleware>();
        }
    }
}
