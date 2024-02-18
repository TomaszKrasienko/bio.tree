namespace bio.tree.server.domain.Exceptions;

public sealed class EmptyEmailException() 
    : BioTreeException("Email can not be empty");