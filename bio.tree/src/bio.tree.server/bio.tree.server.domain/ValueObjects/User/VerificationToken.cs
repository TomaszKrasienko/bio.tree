namespace bio.tree.server.domain.ValueObjects.User;

public record VerificationToken
{
    public string Token { get; }
    public DateTimeOffset ConfirmationDate { get; private set; }

    public VerificationToken(string token)
    {
        Token = token;
    }

    private VerificationToken(string token, DateTimeOffset confirmationDate)
    {
        Token = token;
        ConfirmationDate = confirmationDate;
    }

    internal void Confirm(DateTimeOffset confirmationDate)
        => ConfirmationDate = confirmationDate;
}