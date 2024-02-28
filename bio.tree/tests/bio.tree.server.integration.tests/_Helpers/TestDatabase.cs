using bio.tree.server.infrastructure.DAL.Configuration.Models;
using MongoDB.Driver;

namespace bio.tree.server.integration.tests._Helpers;

public sealed class TestDatabase : IDisposable
{   
    private const string SectionName = "MongoDb";
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    
    public TestDatabase()
    {
        var options = new OptionsProvider().Get<MongoOptions>(SectionName);
        _mongoClient = new MongoClient(options.ConnectionString);
        _mongoDatabase = _mongoClient.GetDatabase(options.Database);
    }

    internal IMongoCollection<T> GetCollection<T>(string collectionName)
        => _mongoDatabase.GetCollection<T>(collectionName);
    
    
    public void Dispose()
    {
        var options = new OptionsProvider().Get<MongoOptions>(SectionName);
        _mongoClient.DropDatabase(options.Database);
    }
}