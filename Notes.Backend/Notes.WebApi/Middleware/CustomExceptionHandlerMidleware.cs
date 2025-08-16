using FluentValidation;
using Notes.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace Notes.WebApi.Middleware
{
    public class CustomExceptionHandlerMidleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandlerMidleware(RequestDelegate next)
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
            var code = HttpStatusCode.InternalServerError; 
            var result = string.Empty;
            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (string.IsNullOrEmpty(result))
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}
