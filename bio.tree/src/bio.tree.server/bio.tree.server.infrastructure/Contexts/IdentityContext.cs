using System.Security.Claims;
using bio.tree.server.infrastructure.Contexts.Abstractions;

namespace bio.tree.server.infrastructure.Contexts;

internal sealed class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    
    public IdentityContext(ClaimsPrincipal claimsPrincipal)
    {
        IsAuthenticated = claimsPrincipal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(claimsPrincipal.Identity?.Name!) : Guid.Empty;
    }
}