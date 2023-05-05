using AutoHunie.ConsoleApp;
using AutoHunie.Core;
using AutoHunie.Core.Entities;
using AutoHunie.Tests.Data;
using System.Drawing;

namespace AutoHunie.Tests;

public class BoardRecognitionTests
{
    private readonly IImageProcessing _imageProcessing;

    public BoardRecognitionTests()
    {
        _imageProcessing = new WindowsImageProcessing();
    }

    [Theory]
    [ClassData(typeof(BoardRecognitionData))]
    public void TestBoards(string imageFilePath, string csvFilePath)
    {
        var boardBitmap = _imageProcessing.CropToGameBoard(new Bitmap(imageFilePath));
        GameBoard expectedBoard = GetGameBoardFromCSV(csvFilePath);

        GameBoard actualBoard = _imageProcessing.RecognizeBoard(boardBitmap);

        Assert.Equal(expectedBoard, actualBoard);
    }

    private static GameBoard GetGameBoardFromCSV(string csvFilePath)
    {
        var rawCsv = File.ReadAllLines(csvFilePath);
        var expectedTokens = rawCsv.Select(l => l.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(e => Enum.Parse<TokenType>(e)));
        var expectedBoard = new GameBoard();
        for (var y = 0; y < expectedTokens.Count(); y++)
        {
            var row = expectedTokens.ElementAt(y);
            expectedBoard.SetRow(y, row.Select(t => new Token(t)).ToArray());
        }

        return expectedBoard;
    }
}
