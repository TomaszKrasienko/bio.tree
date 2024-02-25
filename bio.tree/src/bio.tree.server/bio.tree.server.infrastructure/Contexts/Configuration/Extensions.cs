using bio.tree.server.infrastructure.Contexts.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.Contexts.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddContexts(this IServiceCollection services)
        => services
            .AddSingleton<IIdentityContextFactory, IdentityContextFactory>()
            .AddTransient<IIdentityContext>(sp => sp.GetRequiredService<IIdentityContextFactory>().Create());
}