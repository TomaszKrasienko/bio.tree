namespace bio.tree.server.domain.Exceptions;

public sealed class UserLinkWithThisPlatformAlreadyExistsException(Guid platformId)
    : BioTreeException($"User has registered link to platform with ID: {platformId}");