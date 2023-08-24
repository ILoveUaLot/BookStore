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
                // Read the request body, expecting JSON content
                var requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
                var order = JsonConvert.DeserializeObject<OrderModel>(requestBody);

                // Log information about the order and the ordered books
                _logger.LogInformation($"Made order: {order.Id}; " +
                    $"Order accepted for processing {order.OrderDate}");
                foreach (var item in order.Books)
                {
                    _logger.LogInformation($"{item.Title} was ordered; it is id {item.Id}");
                }
            }
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
