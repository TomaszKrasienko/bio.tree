using System.Net.Http.Headers;
using bio.tree.server.application.Services;
using bio.tree.server.infrastructure.Security;
using bio.tree.server.infrastructure.Security.Configuration.Models;
using bio.tree.server.integration.tests._Helpers;
using bio.tree.server.tests.shared.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace bio.tree.server.integration.tests.Controllers;

public abstract class BaseTestController : IDisposable
{
    private readonly IAuthenticator _authenticator;
    protected HttpClient Client { get; set; }
    protected TestDatabase TestDatabase { get; set; }
    
    public BaseTestController()
    {
        var app = new TestApp(ConfigureServices);
        var options = Options.Create(new OptionsProvider().Get<JwtOptions>("Jwt"));
        _authenticator = new JwtAuthenticator(TestClock.Get(), options);
        Client = app.HttpClient;
        TestDatabase = new TestDatabase();
    }
    
    protected virtual void ConfigureServices(IServiceCollection services)
    {
        
    }

    protected void Authorize(Guid userId)
    {
        var jwt = _authenticator.CreateToken(userId);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt.Token);
    }

    public void Dispose()
    {
        TestDatabase.Dispose();
    }
}