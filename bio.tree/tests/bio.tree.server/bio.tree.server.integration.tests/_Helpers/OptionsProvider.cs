using bio.tree.server.infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace bio.tree.server.integration.tests._Helpers;

public sealed class OptionsProvider
{
    private readonly IConfiguration _configuration;
    
    public OptionsProvider()
    {
        _configuration = GetConfigurationRoot();
    }

    public T Get<T>(string section) where T : class, new() 
        => _configuration.GetOptions<T>(section);

    private static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.Tests.json", true)
            .AddEnvironmentVariables()
            .Build();
}