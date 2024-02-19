using bio.tree.server.domain.Exceptions;
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
    private readonly HashSet<UserLink> _userLinks;
    public IEnumerable<UserLink> UserLinks => _userLinks;
    

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
    
    private User(EntityId id) : base(id)
    {
        _userLinks = new HashSet<UserLink>();
    }

    private User(EntityId id, FullName fullName, Email email, Nickname nickname, Password password)
        : this(id)
    {
        FullName = fullName;
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public static User Create(Guid id, string email, string firstName, string lastName, string nickname,
        string password)
        => new User(id, new FullName(firstName, lastName), email, nickname, password);

    public void AddLink(Guid userLinkId, Guid platformId, string url)
    {
        if (_userLinks.Any(x => x.PlatformId == platformId))
        {
            throw new UserLinkWithThisPlatformAlreadyExistsException(platformId);
        }
        _userLinks.Add(UserLink.Create(userLinkId, platformId, url));
    }
}