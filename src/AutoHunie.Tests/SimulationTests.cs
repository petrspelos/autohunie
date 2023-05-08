using AutoHunie.Core.Entities;
using AutoHunie.Core;

namespace AutoHunie.Tests;

public class SimulationTests
{
    [Fact]
    public void ExploratoryTest()
    {
        var board = new GameBoard(3, 3);

        board.SetRow(0, new[] { new Token(TokenType.BrokenHeart), new Token(TokenType.Joy), new Token(TokenType.Talent) });
        board.SetRow(1, new[] { new Token(TokenType.Sentiment), new Token(TokenType.BrokenHeart), new Token(TokenType.Joy) });
        board.SetRow(2, new[] { new Token(TokenType.BrokenHeart), new Token(TokenType.Joy), new Token(TokenType.Flirtation) });

        var move = board.FindBestMoveConsideringFuture(2);
    }
}
