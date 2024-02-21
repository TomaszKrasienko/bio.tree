using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.application.Exceptions;

public sealed class UserCanNotBeLoggedException(string email)
    : BioTreeException($"User with email: {email} does not exists");