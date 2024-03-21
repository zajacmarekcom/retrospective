namespace Retro.Module.Board.Domain.Services;

public class NewBoardService(IBoardRepository boardRepository)
{
    public async Task<Guid> OpenNewBoard(Guid teamId)
    {
        var previousBoard = await boardRepository.GetCurrentBoard(teamId);
        var newBoard = Board.Create(teamId, previousBoard?.Id);
        boardRepository.Add(newBoard);
        if (previousBoard is not null)
        {
            previousBoard.Close();
            boardRepository.Update(previousBoard);
        }

        await boardRepository.Save();
        
        return newBoard.Id;
    }
}