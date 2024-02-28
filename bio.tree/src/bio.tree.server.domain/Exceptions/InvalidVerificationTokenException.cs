namespace bio.tree.server.domain.Exceptions;

public sealed class InvalidVerificationTokenException(Guid id) 
    : BioTreeException($"Verification token for user with ID: {id} is invalid");