using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.domain.ValueObjects.Platform;

public sealed record Icon
{
    public string Value { get; }

    public Icon(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyPlatformIconException();
        }

        Value = value;
    }

    public static implicit operator Icon(string value) => new Icon(value);
    public static implicit operator string(Icon icon) => icon.Value;
}