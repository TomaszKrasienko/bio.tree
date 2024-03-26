using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.application.Exceptions;

public sealed class PlatformAlreadyExistsException(string name) 
    : BioTreeException($"Platform with name: {name} already exists");