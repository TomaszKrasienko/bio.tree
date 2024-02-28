using bio.tree.server.domain.ValueObjects;
using bio.tree.server.domain.ValueObjects.Platform;

namespace bio.tree.server.domain.Models;

public sealed class Platform : Entity
{
    public string Name { get; private set; }
    
    private Platform(EntityId id) : base(id)
    {
    }

    public Platform(EntityId id, Name name) : this(id)
    {
        Name = name;
    }

    public static Platform Create(Guid id, string name)
        => new Platform(id, name);
}