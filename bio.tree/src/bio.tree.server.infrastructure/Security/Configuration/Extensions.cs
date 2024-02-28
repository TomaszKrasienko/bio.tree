using System.Text;
using bio.tree.server.application.Services;
using bio.tree.server.domain.Models;
using bio.tree.server.infrastructure.Configuration;
using bio.tree.server.infrastructure.Security.Configuration.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace bio.tree.server.infrastructure.Security.Configuration;

internal static class Extensions
{
    private const string SectionName = "Jwt";
    
    internal static IServiceCollection AddSecurity(this IServiceCollection services, 
        IConfiguration configuration)
        => services
            .AddServices()
            .AddOptions(configuration)
            .AddJwtAuthentication(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>()
            .AddSingleton<IAuthenticator, JwtAuthenticator>();

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<JwtOptions>(configuration.GetSection(SectionName));

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = configuration.GetOptions<JwtOptions>(SectionName);

        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Audience = options.Audience;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,
                    ValidateAudience = true,
                    ValidAudience = options.Audience,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
                    ValidateLifetime = true
                };
            });

        services.AddAuthorization();
        return services;
    }
}