using bio.tree.server.domain.Models;

namespace bio.tree.server.domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<bool> ExistsAsync(string email);
    Task<User> GetByVerificationTokenAsync(string token);
    Task<User> GetByEmailAsync(string email);
}