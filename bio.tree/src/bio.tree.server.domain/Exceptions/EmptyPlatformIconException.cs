namespace bio.tree.server.domain.Exceptions;

public sealed class EmptyPlatformIconException()
    : BioTreeException("Platform icon can not be null or empty");