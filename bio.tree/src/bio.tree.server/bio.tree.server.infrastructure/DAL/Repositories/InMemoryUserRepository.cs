using bio.tree.server.domain.Models;
using bio.tree.server.domain.Repositories;

namespace bio.tree.server.infrastructure.DAL.Repositories;

internal sealed class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users;

    public InMemoryUserRepository()
    {
        _users = new List<User>();
    }
    
    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
        => Task.CompletedTask;

    public Task<bool> ExistsAsync(string email)
        => Task.FromResult(_users.Any(x => x.Email == email));

    public Task<User> GetByVerificationTokenAsync(string token)
        => Task.FromResult(_users.FirstOrDefault(x => x.VerificationToken.Token == token));

    public Task<User> GetByEmailAsync(string email)
        => Task.FromResult(_users.FirstOrDefault(x => x.Email == email));
}