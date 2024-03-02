namespace Retro.Common.Domain;

public abstract class Entity<TId>(TId id)
{
    protected TId Id { get; init; } = id;
}