using BookStoreAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookStoreAPI.Middlewares
{
    public class OrderValidationMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<OrderValidationMiddleware> _logger;
        public OrderValidationMiddleware(RequestDelegate next, ILogger<OrderValidationMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/api/order", StringComparison.OrdinalIgnoreCase) &&
                httpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                using (var reader = new StreamReader(httpContext.Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();

                    var order = JsonConvert.DeserializeObject<OrderModel>(requestBody);

                    var validationContext = new ValidationContext(order);
                    var validationResults = new List<ValidationResult>();

                    if (!Validator.TryValidateObject(order, validationContext, validationResults, true))
                    {
                        var errors = validationResults.Select(result => result.ErrorMessage).ToList();
                        var errorResponse = new ErrorResponse { Errors = errors };
                        var jsonResponse = JsonConvert.SerializeObject(errorResponse);

                        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        httpContext.Response.ContentType = "application/json";
                        await httpContext.Response.WriteAsync(jsonResponse);
                        return;
                    }

                }
                await _next.Invoke(httpContext);
            }
        }
    }


    public class ErrorResponse
    {
        public List<string> Errors { get; set; }
    }

    public static class OrderValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseOrderValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OrderValidationMiddleware>();
        }
    }
}
