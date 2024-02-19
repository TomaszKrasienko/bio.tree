using bio.tree.server.domain.ValueObjects;

namespace bio.tree.server.domain.Models;

public sealed class UserLink
{
    public EntityId UserLinkId { get; set; }
    public EntityId PlatformId { get; set; }
    public Url Url { get; set; }

    private UserLink(EntityId userLinkId, EntityId platformId, Url url)
    {
        UserLinkId = userLinkId;
        PlatformId = platformId;
        Url = url;
    }

    internal static UserLink Create(Guid userLinkId, Guid platformId, string url)
        => new UserLink(userLinkId, platformId, url);

}