using bio.tree.server.application.DTO;

namespace bio.tree.server.application.Services;

public interface ITokenStorage
{
    void Set(JwtTokenDto dto);
}