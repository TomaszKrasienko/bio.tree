using bio.tree.server.integration.tests._Helpers;
using Microsoft.Extensions.DependencyInjection;
namespace bio.tree.server.integration.tests.Controllers;

public abstract class BaseTestController : IDisposable
{
    protected HttpClient Client { get; set; }
    protected TestDatabase TestDatabase { get; set; }
    
    public BaseTestController()
    {
        var app = new TestApp(ConfigureServices);
        Client = app.HttpClient;
        TestDatabase = new TestDatabase();
    }
    
    protected virtual void ConfigureServices(IServiceCollection services)
    {
        
    }

    public void Dispose()
    {
        TestDatabase.Dispose();
    }
}