namespace Shared.Domain;

public class Entity<TId>
{
    public TId Id { get; }

    protected Entity()
    {
    }

    public Entity(TId id) : this()
    {
        Id = id;
    }

    public override bool Equals(object? obj) => obj is Entity<TId> other && EqualityComparer<TId>.Default.Equals(Id, other.Id);

    public override int GetHashCode() => Id!.GetHashCode();
}