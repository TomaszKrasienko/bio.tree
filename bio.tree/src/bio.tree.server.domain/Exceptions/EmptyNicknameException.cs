namespace bio.tree.server.domain.Exceptions;

public sealed class EmptyNicknameException() 
    : BioTreeException("Nickname can not be empty");