using bio.tree.server.domain.Models;
using bio.tree.server.domain.Repositories;
using bio.tree.server.infrastructure.DAL.Documents;
using bio.tree.server.infrastructure.DAL.Documents.Mappers;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace bio.tree.server.infrastructure.DAL.Repositories;

internal sealed class MongoUserRepository : IUserRepository
{
    private const string CollectionName = "users";
    private readonly IMongoCollection<UserDocument> _collection;
    
    public MongoUserRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<UserDocument>(CollectionName);
    }

    public Task AddAsync(User user)
        => _collection.InsertOneAsync(user.AsDocument());

    public Task UpdateAsync(User user)
        => _collection.FindOneAndReplaceAsync(x => x.Id.Equals(user.Id), user.AsDocument());

    public Task<bool> ExistsAsync(string email)
        => _collection.Find(x => x.Email == email).AnyAsync();

    public async Task<User> GetByVerificationTokenAsync(string token)
        => (await _collection.Find(x => x.VerificationToken == token)?
            .FirstOrDefaultAsync()!)?.AsEntity();

    public async Task<User> GetByEmailAsync(string email)
        => (await _collection.Find(x => x.Email == email)?
            .FirstOrDefaultAsync()!)?.AsEntity();
}