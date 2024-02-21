using bio.tree.server.application.DTO;
using bio.tree.server.application.Services;

namespace bio.tree.server.infrastructure.Storage;

internal sealed class TokenStorage : ITokenStorage
{
    private JwtTokenDto _dto;

    public void Set(JwtTokenDto dto)
        => _dto = dto;

    public JwtTokenDto Get()
        => _dto;
}