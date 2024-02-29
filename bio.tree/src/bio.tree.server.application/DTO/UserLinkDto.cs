namespace bio.tree.server.application.DTO;

public class UserLinkDto
{
    public Guid Id { get; set; }
    public Guid PlatformId { get; set; }
    public string Url { get; set; }
}