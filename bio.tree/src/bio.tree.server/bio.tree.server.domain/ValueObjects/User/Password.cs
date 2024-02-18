namespace bio.tree.server.domain.ValueObjects.User;

public record Password
{
    public string Value { get; }
    
    public Password(string value)
    {
        Value = value;
    }
}