using bio.tree.server.domain.Repositories;
using bio.tree.server.infrastructure.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.DAL.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
        => services.AddSingleton<IUserRepository, InMemoryUserRepository>();
}