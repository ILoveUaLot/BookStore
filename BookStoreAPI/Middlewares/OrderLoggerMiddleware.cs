using BookStoreAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookStoreAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class OrderLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<OrderLoggerMiddleware> _logger;
        public OrderLoggerMiddleware(RequestDelegate next, ILogger<OrderLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api/order", StringComparison.OrdinalIgnoreCase) &&
                httpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                using (var reader = new StreamReader(httpContext.Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();

                    var order = JsonConvert.DeserializeObject<OrderModel>(requestBody);

                    _logger.LogInformation($"Maked order: {order.Id}; " +
                        $"Order accepted for processing {order.OrderDate}");
                    foreach (var item in order.Books)
                    {
                        _logger.LogInformation($"{item.Title} was ordered; it is id {item.Id}");
                    }

                    await _next(httpContext);
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
