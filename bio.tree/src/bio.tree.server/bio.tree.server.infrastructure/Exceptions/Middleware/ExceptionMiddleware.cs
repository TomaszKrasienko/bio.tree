using bio.tree.server.application.Exceptions;
using bio.tree.server.domain.Exceptions;
using bio.tree.shared;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace bio.tree.server.infrastructure.Exceptions.Middleware;

internal sealed class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(exception, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            AuthorizeException => (StatusCodes.Status400BadRequest, new ErrorDto
            {
               Exception = GetException(nameof(AuthorizeException)),
               Message = "Wrong credentials"
            }),
            BioTreeException => (StatusCodes.Status400BadRequest, new ErrorDto
            {
                Exception = GetException(exception.GetType().Name),
                Message = exception.Message
            }),
            _ => (StatusCodes.Status500InternalServerError, new ErrorDto
            {
                Exception = "server error", 
                Message = "There was an error"
            })
        };
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private string GetException(string name)
        => name.Underscore().ToLower().Replace("_exception", "");
}