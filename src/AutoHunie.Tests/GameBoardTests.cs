using AutoHunie.Core.Entities;

namespace AutoHunie.Tests;

public class GameBoardTests
{
    [Fact]
    public void NewBoard_ShouldBeOfRightSize()
    {
        var board = new GameBoard(numOfColumns: 100, numOfRows: 50);

        Assert.Equal(100, board.Width);
        Assert.Equal(50, board.Height);
        Assert.Equal(100, board.GetRow(0).Length);
        Assert.Equal(50, board.GetColumn(0).Length);
    }

    [Fact]
    public void NewBoard_ShouldHaveRightDefaults()
    {
        var board = new GameBoard();

        Assert.Equal(GameBoard.NumberOfColumns, board.Width);
        Assert.Equal(GameBoard.NumberOfRows, board.Height);
        Assert.Equal(GameBoard.NumberOfColumns, board.GetRow(0).Length);
        Assert.Equal(GameBoard.NumberOfRows, board.GetColumn(0).Length);
    }

    [Fact]
    public void References_ShouldMakeSense()
    {
        var board1 = new GameBoard();
        board1.SetToken(1, 1, new Token(Core.TokenType.Joy));

        var board2 = new GameBoard();
        board2.SetToken(1, 1, new Token(Core.TokenType.Joy));

        Assert.Equal(board1, board2);
        Assert.True(board1 == board2);

        Assert.NotEqual(board1, null);
        Assert.NotEqual(null, board1);

        Assert.False(board1 != board2);
    }

    [Fact]
    public void TokenTypeComparison_ShouldMakeSense()
    {
        var token1 = new Token(Core.TokenType.Passion);
        var token2 = new Token(Core.TokenType.Passion);

        Assert.Equal(token1, token2);
        Assert.True(token1 == token2);

        Assert.NotEqual(null, token1);
        Assert.NotEqual(token1, null);
        
        Assert.False(null! == token1);
        Assert.False(token1 == null!);
    }
}
