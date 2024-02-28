using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.application.Exceptions;

public sealed class EmailAlreadyRegisteredException(string email) 
    : BioTreeException($"Email: {email} is already registered");