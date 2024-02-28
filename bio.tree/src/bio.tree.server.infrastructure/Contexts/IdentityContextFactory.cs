using bio.tree.server.infrastructure.Contexts.Abstractions;
using Microsoft.AspNetCore.Http;

namespace bio.tree.server.infrastructure.Contexts;

internal sealed class IdentityContextFactory(
    IHttpContextAccessor httpContextAccessor) : IIdentityContextFactory
{
    public IIdentityContext Create()
    {
        var httpContext = httpContextAccessor.HttpContext;
        return new IdentityContext(httpContext?.User);
    }
}