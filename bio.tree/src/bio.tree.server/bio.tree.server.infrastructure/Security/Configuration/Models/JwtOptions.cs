namespace bio.tree.server.infrastructure.Security.Configuration;

public record JwtOptions
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public TimeSpan Expiry { get; init; }
    public string SigningKey { get; init; }
}