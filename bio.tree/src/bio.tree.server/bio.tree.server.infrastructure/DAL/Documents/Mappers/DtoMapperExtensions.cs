using bio.tree.shared.DTO;

namespace bio.tree.server.infrastructure.DAL.Documents.Mappers;

internal static class DtoMapperExtensions
{
    internal static UserDto AsDto(this UserDocument document)
        => new UserDto()
        {
            Id = document.Id,
            FirstName = document.FirstName,
            LastName = document.LastName,
            Email = document.LastName,
            Nickname = document.Nickname,
            UserLinks = document.UserLinks?.Select(x => x.AsDto())
        };

    private static UserLinkDto AsDto(this UserLinkDocument document)
        => new UserLinkDto()
        {
            Id = document.Id,
            PlatformId = document.PlatformId,
            Url = document.Url
        };
}