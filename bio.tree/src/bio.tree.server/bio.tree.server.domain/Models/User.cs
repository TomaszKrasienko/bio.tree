using bio.tree.server.domain.ValueObjects;
using bio.tree.server.domain.ValueObjects.User;

namespace bio.tree.server.domain.Models;

public sealed class User : Entity
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Nickname Nickname { get; private set; }
    public Password Password { get; private set; }
    public VerificationToken VerificationToken { get; private set; }
    public ResetToken ResetToken { get; private set; }

    private User(EntityId id, FullName fullName, Email email, Nickname nickname, Password password, 
        VerificationToken verificationToken, ResetToken resetToken) : base(id)
    {
        FullName = fullName;
        Email = email;
        Nickname = nickname;
        Password = password;
        VerificationToken = verificationToken;
        ResetToken = resetToken;
    }

    internal User(EntityId id, Email email) : base(id)
        => Email = email;

    public static User Create(Guid id, string email, string firstName, string lastName, string nickname,
        string password)
    {
        
    }
}