using bio.tree.client.Auth.Abstractions;
using Blazored.LocalStorage;

namespace bio.tree.client.Auth.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddAuth(this IServiceCollection services)
        => services
            .AddServices()
            .AddBlazoredLocalStorage();

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddScoped<IAuthService, AuthService>();
}