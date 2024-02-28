using bio.tree.shared.DTO;

namespace bio.tree.server.application.Services;

public interface ITokenStorage
{
    void Set(JwtTokenDto dto);
    JwtTokenDto Get();
}