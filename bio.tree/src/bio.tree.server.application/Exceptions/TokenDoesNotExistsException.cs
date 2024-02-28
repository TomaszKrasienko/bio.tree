using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.application.Exceptions;

public sealed class TokenDoesNotExistsException()
    : AuthorizeException("Token does not exists");