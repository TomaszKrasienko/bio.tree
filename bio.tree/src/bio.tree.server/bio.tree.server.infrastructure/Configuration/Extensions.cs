using bio.tree.server.infrastructure.CQRS.Extensions;
using bio.tree.server.infrastructure.DAL.Configuration;
using bio.tree.server.infrastructure.Security.Configuration;
using bio.tree.server.infrastructure.Storage.Configuration;
using bio.tree.server.infrastructure.Time.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.Configuration;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDal(configuration)
            .AddSecurity(configuration)
            .AddStorage()
            .AddTime()
            .AddCqrs()
            .AddControllersConfiguration();

    private static IServiceCollection AddControllersConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}