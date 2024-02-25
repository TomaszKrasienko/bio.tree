namespace bio.tree.server.infrastructure.DAL.Documents;

internal sealed class UserLinkDocument
{
    public Guid Id { get; set; }
    public Guid PlatformId { get; set; }
    public string Url { get; set; }
}