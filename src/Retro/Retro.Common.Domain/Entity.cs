namespace Retro.Common.Domain;

public abstract class Entity<TId>(TId id)
{
    public TId Id { get; } = id;
}