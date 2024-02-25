using bio.tree.client.Auth.Abstractions;
using bio.tree.client.Configuration;
using bio.tree.client.Services.Abstractions;
using bio.tree.client.Services.Configuration.Models;

namespace bio.tree.client.Services.Configuration;

internal static class Extensions
{
    private const string ClientName = "Api";
    private const string SectionName = "HttpClient";

    public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddServices()
            .AddClient(configuration);
    
    private static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddScoped<IUserService>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var authService = sp.GetRequiredService<IAuthService>();
            return new UserService(authService, httpClientFactory.CreateClient(ClientName));
        });
    
    private static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<HttpClientOptions>(SectionName);
        services.AddHttpClient(ClientName, builder =>
        {
            builder.BaseAddress = new Uri(options.Url);
            builder.Timeout = options.Timeout;
        });
        return services;
    }
}