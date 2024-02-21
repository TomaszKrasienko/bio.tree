using bio.tree.server.application.Services;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.Time.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddTime(this IServiceCollection services)
        => services.AddSingleton<IClock, Clock>();
}