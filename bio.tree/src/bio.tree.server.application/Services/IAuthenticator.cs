using bio.tree.shared.DTO;

namespace bio.tree.server.application.Services;

public interface IAuthenticator
{
    JwtTokenDto CreateToken(Guid userId);
}