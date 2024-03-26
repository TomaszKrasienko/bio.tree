using bio.tree.server.domain.ValueObjects;
using bio.tree.server.domain.ValueObjects.Platform;

namespace bio.tree.server.domain.Models;

public sealed class Platform : Entity
{
    public Name Name { get; private set; }
    public Icon Icon { get; private set; }
    
    private Platform(EntityId id) : base(id)
    {
    }

    public Platform(EntityId id, Name name, Icon icon) : this(id)
    {
        Name = name;
        Icon = icon;
    }

    public static Platform Create(Guid id, string name, string icon)
        => new Platform(id, name, icon);
}