using AutoHunie.Core;
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
        board1.SetToken(1, 1, new Token(TokenType.Joy));

        var board2 = new GameBoard();
        board2.SetToken(1, 1, new Token(TokenType.Joy));

        Assert.Equal(board1, board2);
        Assert.True(board1 == board2);

        Assert.False(board1 != board2);
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

        var board2 = board1.Clone();
        board2.SetToken(1, 1, new Token(TokenType.Passion));

        Assert.Equal(TokenType.Joy, board1.GetToken(1, 1).Type);
        Assert.Equal(TokenType.Passion, board2.GetToken(1, 1).Type);
    }

    [Fact]
    public void ColumnSetting_ShouldWork()
    {
        var board = new GameBoard(3, 2);

        board.SetRow(1, new [] { new Token(TokenType.BrokenHeart), new Token(TokenType.Flirtation), new Token(TokenType.Passion) });

        var row = board.GetRow(1);

        Assert.Equal(new Token(TokenType.BrokenHeart), row[0]);
        Assert.Equal(new Token(TokenType.Flirtation), row[1]);
        Assert.Equal(new Token(TokenType.Passion), row[2]);

        board.SetColumn(1, new [] { new Token(TokenType.Romance), new Token(TokenType.Stamina) });

        var column = board.GetColumn(1);

        Assert.Equal(new Token(TokenType.Romance), column[0]);
        Assert.Equal(new Token(TokenType.Stamina), column[1]);

        board.SetColumn(1, new [] { new Token(TokenType.Romance), new Token(TokenType.Joy) });

        column = board.GetColumn(1);

        Assert.Equal(new Token(TokenType.Romance), column[0]);
        Assert.Equal(new Token(TokenType.Joy), column[1]);
    }

    [Fact]
    public void GameBoardSimulation_ShouldPopTokens()
    {
        var gameBoard = new GameBoard(6, 4);

        gameBoard.SetRow(0, new Token[] { new(TokenType.Stamina), new(TokenType.Joy), new(TokenType.BrokenHeart), new(TokenType.Sentiment), new(TokenType.Passion), new(TokenType.Flirtation) });
        gameBoard.SetRow(1, new Token[] { new(TokenType.Talent), new(TokenType.Passion), new(TokenType.Romance), new(TokenType.Sexuality), new(TokenType.Flirtation), new(TokenType.Flirtation) });
        gameBoard.SetRow(2, new Token[] { new(TokenType.Romance), new(TokenType.Romance), new(TokenType.Romance), new(TokenType.Talent), new(TokenType.Sexuality), new(TokenType.Flirtation) });
        gameBoard.SetRow(3, new Token[] { new(TokenType.Joy), new(TokenType.Stamina), new(TokenType.Romance), new(TokenType.Passion), new(TokenType.Sentiment), new(TokenType.Talent) });

        var expectedBoard = new GameBoard(6, 4);

        var beforeString = gameBoard.ToString();

        expectedBoard.SetRow(0, new Token[] { new(TokenType.Unknown), new(TokenType.Unknown), new(TokenType.Unknown), new(TokenType.Sentiment), new(TokenType.Passion), new(TokenType.Unknown) });
        expectedBoard.SetRow(1, new Token[] { new(TokenType.Stamina), new(TokenType.Joy), new(TokenType.Unknown), new(TokenType.Sexuality), new(TokenType.Flirtation), new(TokenType.Unknown) });
        expectedBoard.SetRow(2, new Token[] { new(TokenType.Talent), new(TokenType.Passion), new(TokenType.Unknown), new(TokenType.Talent), new(TokenType.Sexuality), new(TokenType.Unknown) });
        expectedBoard.SetRow(3, new Token[] { new(TokenType.Joy), new(TokenType.Stamina), new(TokenType.BrokenHeart), new(TokenType.Passion), new(TokenType.Sentiment), new(TokenType.Talent) });

        var score = gameBoard.SimulateForward();

        Assert.Equal(expectedBoard, gameBoard);
    }
}
