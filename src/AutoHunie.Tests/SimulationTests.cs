using AutoHunie.Core;
using AutoHunie.Core.Entities;

namespace AutoHunie.Tests;

public class SimulationTests
{
    [Fact]
    public void GameSolver_ShouldProduceBestPossibleMove()
    {
        var gameBoard = new GameBoard(6, 4);

        gameBoard.SetRow(0, new Token[] { new(TokenType.Stamina), new(TokenType.Joy), new(TokenType.BrokenHeart), new(TokenType.Sentiment), new(TokenType.Passion), new(TokenType.Flirtation) });
        gameBoard.SetRow(1, new Token[] { new(TokenType.Talent), new(TokenType.Passion), new(TokenType.Romance), new(TokenType.Sexuality), new(TokenType.Flirtation), new(TokenType.Joy) });
        gameBoard.SetRow(2, new Token[] { new(TokenType.Romance), new(TokenType.Romance), new(TokenType.Sexuality), new(TokenType.Talent), new(TokenType.Sexuality), new(TokenType.Flirtation) });
        gameBoard.SetRow(3, new Token[] { new(TokenType.Joy), new(TokenType.Stamina), new(TokenType.BrokenHeart), new(TokenType.Passion), new(TokenType.Sentiment), new(TokenType.Talent) });

        Assert.Equal(new Token(TokenType.Sexuality), gameBoard.GetToken(2, 2));
        Assert.Equal(new Token(TokenType.Sexuality), gameBoard.GetToken(3, 1));
        Assert.Equal(new Token(TokenType.Sexuality), gameBoard.GetToken(4, 2));
        Assert.Equal(new Token(TokenType.Stamina), gameBoard.GetToken(0, 0));

        var solver = new GameSolver(gameBoard);

        var move = solver.GetNextBestMove(null!);

        var newBoard = gameBoard.ApplyMove(move);

        Assert.True(newBoard.HasMatch());
        Assert.Equal(new Token(TokenType.Sexuality), gameBoard.GetToken(2, 2));
        Assert.Equal(new Token(TokenType.Sexuality), gameBoard.GetToken(3, 2));
        Assert.Equal(new Token(TokenType.Sexuality), gameBoard.GetToken(4, 2));
    }
}
