using bio.tree.server.domain.Exceptions;

namespace bio.tree.server.domain.ValueObjects;

public sealed record EntityId
{
    public Guid Value { get; set; }
    public EntityId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyEntityIdException();
        }
        Value = value;
    }
    
    public static implicit operator Guid(EntityId entityId)
        => entityId.Value;

    public static implicit operator EntityId(Guid value)
        => new EntityId(value);
    
    public bool IsEmpty() => Value == Guid.Empty;
    
    public static bool operator == (EntityId entityId, Guid guid)
        => entityId is not null && entityId.Value == guid;

    public static bool operator !=(EntityId entityId, Guid guid) 
        => entityId is null || !(entityId.Value == guid);
}