using System.Security.Cryptography;

namespace bio.tree.server.domain.ValueObjects.User;

public record VerificationToken
{
    public string Token { get; }
    public DateTimeOffset? ConfirmationDate { get; private set; }

    public VerificationToken()
    {
        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
            .Replace("/", "")
            .Replace("==", "");;
    }

    private VerificationToken(string token, DateTimeOffset confirmationDate)
    {
        Token = token;
        ConfirmationDate = confirmationDate;
    }

    internal void Confirm(DateTimeOffset confirmationDate)
        => ConfirmationDate = confirmationDate;
}