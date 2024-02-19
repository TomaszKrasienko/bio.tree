namespace bio.tree.server.domain.Exceptions;

public sealed class EmptyPlatformNameException() 
    : BioTreeException("Platform name can not be empty");