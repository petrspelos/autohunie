using AutoHunie.Core;
using AutoHunie.Core.Entities;

namespace AutoHunie.Tests;

public class GameBoardTests
{
    [Fact]
    public void NewBoard_ShouldBeOfRightSize()
    {
        var board = new GameBoard(numOfColumns: 100, numOfRows: 50);

        Assert.Equal(100, board.Columns);
        Assert.Equal(50, board.Rows);
        Assert.Equal(100, board.GetRow(0).Length);
        Assert.Equal(50, board.GetColumn(0).Length);
    }

    [Fact]
    public void NewBoard_ShouldHaveRightDefaults()
    {
        var board = new GameBoard();

        Assert.Equal(GameBoard.NumberOfColumns, board.Columns);
        Assert.Equal(GameBoard.NumberOfRows, board.Rows);
        Assert.Equal(GameBoard.NumberOfColumns, board.GetRow(0).Length);
        Assert.Equal(GameBoard.NumberOfRows, board.GetColumn(0).Length);
    }

    [Fact]
    public void TokenTypeComparison_ShouldMakeSense()
    {
        var token1 = new Token(TokenType.Passion);
        var token2 = new Token(TokenType.Passion);

        Assert.Equal(token1, token2);
        Assert.True(token1 == token2);
    }

    [Fact]
    public void ClonedBoard_ShouldNotEffectOriginal()
    {
        var board1 = new GameBoard();
        board1.SetToken(1, 1, new Token(TokenType.Joy));

        var board2 = board1.DeepCopy();
        board2.SetToken(1, 1, new Token(TokenType.Passion));

        Assert.Equal(TokenType.Joy, board1.GetToken(1, 1).Type);
        Assert.Equal(TokenType.Passion, board2.GetToken(1, 1).Type);
    }

    [Fact]
    public void GameBoardEquality_ShouldWork()
    {
        var board = new GameBoard(3, 2);

        board.SetRow(1, new[] { new Token(TokenType.BrokenHeart), new Token(TokenType.Flirtation), new Token(TokenType.Passion) });

        var board2 = new GameBoard(3, 2);

        board2.SetRow(1, new[] { new Token(TokenType.BrokenHeart), new Token(TokenType.Flirtation), new Token(TokenType.Passion) });

        Assert.Equal(board, board2);
    }

    [Fact]
    public void ColumnSetting_ShouldWork()
    {
        var board = new GameBoard(3, 2);

        board.SetRow(1, new[] { new Token(TokenType.BrokenHeart), new Token(TokenType.Flirtation), new Token(TokenType.Passion) });

        var row = board.GetRow(1);

        Assert.Equal(new Token(TokenType.BrokenHeart), row[0]);
        Assert.Equal(new Token(TokenType.Flirtation), row[1]);
        Assert.Equal(new Token(TokenType.Passion), row[2]);

        board.SetColumn(1, new[] { new Token(TokenType.Romance), new Token(TokenType.Stamina) });

        var column = board.GetColumn(1);

        Assert.Equal(new Token(TokenType.Romance), column[0]);
        Assert.Equal(new Token(TokenType.Stamina), column[1]);

        board.SetColumn(1, new[] { new Token(TokenType.Romance), new Token(TokenType.Joy) });

        column = board.GetColumn(1);

        Assert.Equal(new Token(TokenType.Romance), column[0]);
        Assert.Equal(new Token(TokenType.Joy), column[1]);
    }

    [Fact]
    public void HasMatch_GivenNoMatches_ShouldReturnFalse()
    {
        var board = new GameBoard(3, 2);

        board.SetRow(0, new[] { new Token(TokenType.BrokenHeart), new Token(TokenType.Romance), new Token(TokenType.Romance) });
        board.SetRow(1, new[] { new Token(TokenType.Stamina), new Token(TokenType.Passion), new Token(TokenType.BrokenHeart) });

        Assert.False(board.HasMatch());
    }

    [Fact]
    public void FindAllPossibleMoves_GivenBoardWithMatches_ShouldThrow()
    {
        var board = new GameBoard(3, 2);

        board.SetRow(0, new[] { new Token(TokenType.Romance), new Token(TokenType.Romance), new Token(TokenType.Romance) });
        board.SetRow(1, new[] { new Token(TokenType.Stamina), new Token(TokenType.Passion), new Token(TokenType.BrokenHeart) });

        Assert.Throws<InvalidOperationException>(board.FindAllPossibleMoves);
    }
}
