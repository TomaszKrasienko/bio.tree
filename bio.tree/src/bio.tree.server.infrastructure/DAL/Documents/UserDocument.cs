using bio.tree.server.domain.Models;

namespace bio.tree.server.infrastructure.DAL.Documents;

internal sealed class UserDocument
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
    public string VerificationToken { get; set; }
    public DateTimeOffset? VerificationDate { get; set; }
    public string ResetToken { get; set; }
    public DateTimeOffset? ResetTokenExpire { get; set; }
    public IEnumerable<UserLinkDocument> UserLinks { get; set; }
}