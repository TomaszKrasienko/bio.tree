using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.application.Exceptions;

internal class InvalidPasswordException(string email)
    : BioTreeException($"Invalid password for user with email: {email}");