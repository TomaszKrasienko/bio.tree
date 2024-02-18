using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.domain.ValueObjects.User;

public sealed record FullName
{
    public string FirstName { get; }
    public string LastName { get; }

    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new EmptyFirstNameException();
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new EmptyLastNameException();
        }
        FirstName = firstName; 
        LastName = lastName;
    }

    public override string ToString()
        => $"{FirstName} {LastName}";
}

public sealed class EmptyFirstNameException() 
    : BioTreeException("First name can not be empty");