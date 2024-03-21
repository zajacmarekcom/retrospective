using Retro.Common.Domain;

namespace Retro.Module.Board.Domain;

public interface IBoardRepository : IRepository<Board>
{
    Task<Board?> GetCurrentBoard(Guid teamId);
    Task<Board?> GetPreviousBoardAsync(Guid boardId);
}