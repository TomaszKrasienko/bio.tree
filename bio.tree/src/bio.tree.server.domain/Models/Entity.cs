using bio.tree.server.domain.ValueObjects;

namespace bio.tree.server.domain.Models;

public abstract class Entity
{
    public EntityId Id { get; private set; }

    protected Entity(EntityId id)
    {
        Id = id;
    }
}