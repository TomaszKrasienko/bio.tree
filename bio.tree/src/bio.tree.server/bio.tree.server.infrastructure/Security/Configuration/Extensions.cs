using bio.tree.server.application.Services;
using bio.tree.server.domain.Models;
using bio.tree.server.infrastructure.Security.Configuration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.Security.Configuration;

internal static class Extensions
{
    private const string SectionName = "Jwt";
    
    internal static IServiceCollection AddSecurity(this IServiceCollection services, 
        IConfiguration configuration)
        => services
            .AddServices()
            .AddOptions(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddSingleton<IAuthenticator, JwtAuthenticator>();

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<JwtOptions>(configuration.GetSection(SectionName));
}