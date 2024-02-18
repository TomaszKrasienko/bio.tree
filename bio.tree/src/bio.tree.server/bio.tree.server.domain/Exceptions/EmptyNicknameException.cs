using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.domain.ValueObjects.User;

public sealed class EmptyNicknameException() 
    : BioTreeException("Nickname can not be empty");