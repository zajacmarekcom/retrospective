using Retro.Common.Domain;
using Retro.Common.Exceptions;

namespace Retro.Module.Board.Domain;

public class Board : Entity<Guid>
{
    private IList<Guid> _items;
    private Guid? PreviousBoardId { get; set; } = null;
    private Guid TeamId { get; set; }
    private DateTimeOffset CreatedAt { get; set; }
    private DateTimeOffset? ClosedAt { get; set; }

    public Board(Guid id, Guid teamId, DateTimeOffset createdAt, DateTimeOffset? closedAt, IList<Guid> items, Guid? previousBoardId) : base(id)
    {
        TeamId = teamId;
        CreatedAt = createdAt;
        ClosedAt = closedAt;
        _items = items;
        PreviousBoardId = previousBoardId;
    }
    
    public Board(Guid teamId, DateTimeOffset createdAt, DateTimeOffset? closedAt, IList<Guid> items, Guid? previousBoardId) : base(Guid.NewGuid())
    {
        TeamId = teamId;
        CreatedAt = createdAt;
        ClosedAt = closedAt;
        _items = items;
        PreviousBoardId = previousBoardId;
    }
    
    public static Board Create(Guid teamId, Guid? previousBoardId = null)
    {
        return new Board(teamId, DateTimeOffset.UtcNow, null, new List<Guid>(), previousBoardId);
    }
    
    public void Close()
    {
        if (ClosedAt.HasValue)
        {
            throw new DomainException("Board is already closed.");
        }

        ClosedAt = DateTimeOffset.UtcNow;
    }
    
    public void AddItem(Guid itemId)
    {
        if (_items.Contains(itemId))
        {
            throw new DomainException($"Item {itemId} is already in the board {Id}.");
        }
        _items.Add(itemId);
    }
    
    public void RemoveItem(Guid itemId)
    {
        if (!_items.Contains(itemId))
        {
            throw new DomainException($"Item {itemId} is not in the board {Id}.");
        }
        _items.Remove(itemId);
    }
}