using System.Security.Claims;

namespace bio.tree.server.infrastructure.Contexts.Abstractions;

public interface IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
}