namespace bio.tree.server.domain.Exceptions;

public sealed class EmptyEntityIdException()
    : BioTreeException("EntityId can not be empty");