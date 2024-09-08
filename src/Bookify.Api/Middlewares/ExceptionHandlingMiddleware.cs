using System.Runtime.CompilerServices;
using Bookify.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate requestDelegate)
        {
            _logger = logger;
            _next = requestDelegate;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception Occurred: {Message} ", exception.Message);
                var exceptionDetails = GetExceptionDetails(exception);
                var problemDetails = new ProblemDetails
                {
                    Detail = exceptionDetails.Detail,
                    Type = exceptionDetails.Type,
                    Title = exceptionDetails.Title,
                    Status = exceptionDetails.Status
                };

                if (exceptionDetails.Errors is not null)
                {
                    problemDetails.Extensions["Errors"] = exceptionDetails.Errors;
                }

                context.Response.StatusCode = exceptionDetails.Status;

                await context.Response.WriteAsJsonAsync(problemDetails);


            }
        }



        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                ValidationException validationException => new ExceptionDetails(StatusCodes.Status400BadRequest,
                    "ValidationFailure",
                    "Validation Error",
                    "One or More validation Errors Occurred",
                    validationException.Errors),
                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Server Error",
                    "An Unexpected error has occured.",
                    null)
            };
        }
    }

    public record ExceptionDetails(int Status, string Type, string Title, string Detail, IEnumerable<object>? Errors);
}
