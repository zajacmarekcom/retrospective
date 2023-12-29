namespace Retro.Front.Components.Board.Dtos;

public record BoardItemDto(string Title, bool CanVote, int Votes, BoardItemType Type);