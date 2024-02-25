using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.application.Exceptions;

public abstract class AuthorizeException(string message) 
    : BioTreeException(message);