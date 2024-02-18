namespace bio.tree.server.domain.ValueObjects.User;

public record Nickname
{
    public string Value { get; }

    public Nickname(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyNicknameException();
        }
        Value = value;
    }
    
    public static implicit operator string(Nickname nickname)
        => nickname.Value;

    public static implicit operator Nickname(string value)
        => new Nickname(value);
}