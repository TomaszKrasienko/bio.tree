using bio.tree.server.application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.Storage.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddStorage(this IServiceCollection services)
        => services.AddScoped<ITokenStorage, TokenStorage>();
}