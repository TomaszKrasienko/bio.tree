namespace bio.tree.server.domain.Models;

public sealed record Url
{
    public string Value { get; private set; }

    public Url(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyUrlException();
        }
        Value = value;
    }
    
    public static implicit operator Url(string value)
        => new Url(value);

    public static implicit operator string(Url url)
        => url.Value;
}