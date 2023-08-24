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

        public OrderValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();

            OrderModel order = JsonConvert.DeserializeObject<OrderModel>(requestBody);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(order, new ValidationContext(order), validationResults, true);

            if (!isValid)
            {
                var errors = validationResults.Select(vr => vr.ErrorMessage);
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new {Errors = errors}));
                return;
            }
            await _next(httpContext);
        }
    }

    public static class OrderValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseOrderValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OrderValidationMiddleware>();
        }
    }
}
