using bio.tree.server.domain.Exceptions;
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
            BioTreeException => (StatusCodes.Status400BadRequest, new
            {
                Exception = exception.GetType().Name.Underscore().Replace("Exception", ""),
                Message = exception.Message
            }),
            _ => (StatusCodes.Status500InternalServerError, new
            {
                Exception = "server error", 
                Message = "There was an error"
            })
        };
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}