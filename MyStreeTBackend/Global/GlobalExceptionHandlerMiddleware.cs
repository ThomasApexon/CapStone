using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Global
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError($"An unhandled exception has occurred: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "An internal server error occurred.";
            var errors = new List<string> { exception.Message };

            switch (exception)
            {
                case ArgumentNullException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = "A required argument was null.";
                    break;
                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = "Unauthorized access.";
                    break;
                case KeyNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = "Resource not found.";
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "An internal server error occurred.";
                    break;
            }

            context.Response.StatusCode = statusCode;

            var response = new ApiResponse<object>(false, message, null, errors);

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
