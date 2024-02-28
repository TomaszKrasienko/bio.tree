namespace bio.tree.server.domain.Exceptions;

public class InvalidEmailException(string email)
    : BioTreeException($"Email: {email} is invalid");