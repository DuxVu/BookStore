using BookStore_API.Controllers;
using BookStore_API.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BookStoreAPIController> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<BookStoreAPIController> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(CustomValidationException ex)
            {
                context.Response.StatusCode = (int)ex.ErrorCode;
                var responseBody = new
                {
                    message = ex.ErrorMessage
                };
                _logger.LogError(ex.ErrorMessage, ex.ErrorCode);
                await context.Response.WriteAsJsonAsync(responseBody);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var responseBody = new
                {
                    message = "There was an error, please contact support."
                };

                await context.Response.WriteAsJsonAsync(responseBody);
            }
        }
    }
}
