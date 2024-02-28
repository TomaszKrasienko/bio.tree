using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.domain.ValueObjects.Platform;

public record Name
{
    public string Value { get;}

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyPlatformNameException();
        }
        Value = value;
    }

    public static implicit operator string(Name name)
        => name.Value;

    public static implicit operator Name(string value)
        => new Name(value);
}