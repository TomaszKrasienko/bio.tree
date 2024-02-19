using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.domain.ValueObjects.User;

public sealed record Password
{
    public string Value { get; }
    
    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyPasswordException();
        }
        Value = value;
    }

    public static implicit operator string(Password password)
        => password.Value;

    public static implicit operator Password(string value)
        => new Password(value);
}