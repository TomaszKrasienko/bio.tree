using bio.tree.server.infrastructure.Exceptions.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.Exceptions.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddExceptions(this IServiceCollection services)
        => services.AddSingleton<ExceptionMiddleware>();

    internal static WebApplication UseExceptions(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}