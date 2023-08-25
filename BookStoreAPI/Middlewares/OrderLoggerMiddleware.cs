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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api/order") &&
                httpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                httpContext.Request.EnableBuffering();

                var requestBody = await new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, leaveOpen: true).ReadToEndAsync();
                httpContext.Request.Body.Position = 0;

                if (!string.IsNullOrWhiteSpace(requestBody))
                {
                    var order = JsonConvert.DeserializeObject<OrderModel>(requestBody);
                    if (order != null)
                    {
                        _logger.LogInformation($"Made order: {order.Id}; Order accepted for processing {order.OrderDate}");
                    }
                }
            }

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
