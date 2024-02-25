using bio.tree.server.application.Services;
using bio.tree.shared.DTO;
using Microsoft.AspNetCore.Http;

namespace bio.tree.server.infrastructure.Storage;

internal sealed class HttpContextTokenStorage(
    IHttpContextAccessor httpContextAccessor) : ITokenStorage
{
    private const string TokenKey = "user_jwt_token";

    public void Set(JwtTokenDto dto)
        => httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, dto);

    public JwtTokenDto Get()
    {
        if (httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var tokenDto))
        {
            return tokenDto as JwtTokenDto;
        }

        return null;
    }
}