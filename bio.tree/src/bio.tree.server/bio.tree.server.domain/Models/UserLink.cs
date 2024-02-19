using bio.tree.server.domain.ValueObjects;

namespace bio.tree.server.domain.Models;

public sealed class UserLink : Entity
{
    public EntityId PlatformId { get; set; }
    public Url Url { get; set; }

    private UserLink(EntityId id, EntityId platformId, Url url) : base(id)
    {
        PlatformId = platformId;
        Url = url;
    }

    internal static UserLink Create(Guid userLinkId, Guid platformId, string url)
        => new UserLink(userLinkId, platformId, url);

}