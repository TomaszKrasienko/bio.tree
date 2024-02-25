using bio.tree.client.Auth.Configuration;
using bio.tree.client.Services.Configuration;

namespace bio.tree.client.Configuration;

public static class Extensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddHttpServices(configuration)
            .AddAuth();
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        T t = new T();
        configuration.Bind(sectionName, t);
        return t;
    }
}