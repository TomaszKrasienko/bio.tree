using bio.tree.server.domain.Models;

namespace bio.tree.server.domain.Repositories;

public interface IPlatformRepository
{
    Task AddAsync(Platform platform);
    Task<bool> ExistsAsync(string name);
}