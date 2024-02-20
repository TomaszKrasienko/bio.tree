using bio.tree.server.application.DTO;

namespace bio.tree.server.application.Services;

public interface IAuthenticator
{
    JwtTokenDto CreateToken(Guid userId);
}