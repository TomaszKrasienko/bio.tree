using bio.tree.server.infrastructure.Contexts.Configuration;
using bio.tree.server.infrastructure.CQRS.Extensions;
using bio.tree.server.infrastructure.DAL.Configuration;
using bio.tree.server.infrastructure.Exceptions.Configuration;
using bio.tree.server.infrastructure.Security.Configuration;
using bio.tree.server.infrastructure.Storage.Configuration;
using bio.tree.server.infrastructure.Time.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.Configuration;

public static class Extensions
{
    private const string CorsPolicyName = "bio.tree.cors";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDal(configuration)
            .AddSecurity(configuration)
            .AddStorage()
            .AddTime()
            .AddCqrs()
            .AddExceptions()
            .AddContexts()
            .AddControllersConfiguration()
            .AddCorsPolicy();

    private static IServiceCollection AddControllersConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName, policy =>
            {
                policy
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
        => app
            .UseCorsPolicy()
            .UseExceptions()
            .UseSecurity();

    private static WebApplication UseCorsPolicy(this WebApplication app)
    {
        app.UseCors(CorsPolicyName);
        return app;
    }
    
    private static WebApplication UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        T t = new T();
        configuration.Bind(sectionName, t);
        return t;
    }
}