namespace bio.tree.server.domain.Exceptions;

public sealed class EmptyUserRoleException() 
    : BioTreeException("User role can not be null or empty");