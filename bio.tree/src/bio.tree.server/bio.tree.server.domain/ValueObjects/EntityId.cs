namespace bio.tree.server.domain.ValueObjects;

public sealed record EntityId(Guid Value)
{
    public static implicit operator Guid(EntityId entityId)
        => entityId.Value;

    public static implicit operator EntityId(Guid value)
        => new EntityId(value);
    
    public bool IsEmpty() => Value == Guid.Empty;

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}