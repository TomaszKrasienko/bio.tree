using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.domain.Models;

public sealed class EmptyUrlException()
    : BioTreeException("Url can not be empty");