namespace bio.tree.server.infrastructure.DAL.Configuration.Models;

public sealed record MongoOptions
{
    public string ConnectionString { get; init; }
    public string Database { get; init; }
}