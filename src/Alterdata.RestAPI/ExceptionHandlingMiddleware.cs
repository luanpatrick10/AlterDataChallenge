using System.Net;
using System.Text.Json;
using Shared.Exceptions;

namespace Alterdata.RestAPI
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;        

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {                
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = exception switch
            {
                DomainException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                BusinessRuleException => HttpStatusCode.UnprocessableEntity,
                _ => HttpStatusCode.InternalServerError
            };

            string errorMessage = code == HttpStatusCode.InternalServerError
                ? "An unexpected error occurred. Please try again later."
                : exception.Message;

            var result = JsonSerializer.Serialize(new { error = errorMessage });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
