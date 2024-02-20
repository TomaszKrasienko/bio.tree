namespace bio.tree.server.domain.Exceptions;

public sealed class EmptyUrlException()
    : BioTreeException("Url can not be empty");