namespace bio.tree.server.domain.Exceptions;

public sealed class EmptyPasswordException()
    : BioTreeException("Password can not be empty")
{
    
}