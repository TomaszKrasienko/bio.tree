using bio.tree.server.domain.Repositories;
using bio.tree.server.infrastructure.DAL.Configuration.Models;
using bio.tree.server.infrastructure.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace bio.tree.server.infrastructure.DAL.Configuration;

internal static class Extensions
{
    private const string SectionName = "MongoDb";

    internal static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddOptions(configuration)
            .AddServices()
            .AddMongo();
    
    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<MongoOptions>(configuration.GetSection(SectionName));

    private static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddTransient<IUserRepository, MongoUserRepository>();
    
    private static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoClient(options.ConnectionString);
        });
        services.AddTransient(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.Database);
        });
        return services;
    }
}