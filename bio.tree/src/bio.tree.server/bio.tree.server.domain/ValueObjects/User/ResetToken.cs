namespace bio.tree.server.domain.ValueObjects.User;

public sealed record ResetToken
{
    public string Token { get; }
    public DateTimeOffset Expire { get; }

    public ResetToken(string token, DateTimeOffset expire)
    {
        Token = token;
        Expire = expire;
    }
}