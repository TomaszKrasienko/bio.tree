using bio.tree.server.domain.Models;
using bio.tree.server.domain.ValueObjects.User;

namespace bio.tree.server.infrastructure.DAL.Documents.Mappers;

internal static class EntityMapperExtensions
{
    internal static User AsEntity(this UserDocument document)
        => new User
        (
            document.Id,
            new FullName(document.FirstName, document.LastName),
            document.Email, 
            document.Nickname, 
            document.Password,
            new VerificationToken(document.VerificationToken, document.VerificationDate),
            document.ResetToken is  null ? null : new ResetToken(document.ResetToken, document.ResetTokenExpire),
            document.UserLinks?.Select(x => x.AsEntity())
        );

    internal static UserLink AsEntity(this UserLinkDocument document)
        => new UserLink
        (
            document.Id,
            document.PlatformId,
            document.Url
        );
}