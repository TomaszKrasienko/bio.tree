using bio.tree.shared.DTO;

namespace bio.tree.server.infrastructure.DAL.Documents.Mappers;

internal static class DtoMapperExtensions
{
    internal static UserDto AsDto(this UserDocument document)
        => new UserDto()
        {

        };
}