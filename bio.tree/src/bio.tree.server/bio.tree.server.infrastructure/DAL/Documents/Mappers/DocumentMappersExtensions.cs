using bio.tree.server.domain.Models;

namespace bio.tree.server.infrastructure.DAL.Documents.Mappers;

internal static class DocumentMappersExtensions
{
    internal static UserDocument AsDocument(this User user)
        => new UserDocument()
        {
            Id = user.Id,
            FirstName = user.FullName.FirstName,
            LastName = user.FullName.LastName,
            Email = user.Email,
            Nickname = user.Nickname,
            Password = user.Password,
            VerificationToken = user.VerificationToken?.Token,
            VerificationDate = user.VerificationToken?.ConfirmationDate,
            ResetToken = user.ResetToken?.Token,
            ResetTokenExpire = user.ResetToken?.Expire,
            UserLinks = user.UserLinks?.Select(x => x.AsDocument())
        };

    internal static UserLinkDocument AsDocument(this UserLink userLink)
        => new UserLinkDocument()
        {
            Id = userLink.Id,
            PlatformId = userLink.PlatformId,
            Url = userLink.Url
        };
}