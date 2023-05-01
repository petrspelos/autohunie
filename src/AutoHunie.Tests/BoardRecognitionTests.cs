using AutoHunie.ConsoleApp;
using AutoHunie.Core;
using AutoHunie.Core.Entities;
using AutoHunie.Tests.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace AutoHunie.Tests;

public class BoardRecognitionTests
{
    [Theory]
    [ClassData(typeof(BoardRecognitionData))]
    public void TestBoards(string imageFilePath, string csvFilePath)
    {
        // Arrange - Get board bitmap
        var screenBitmap = new Bitmap(imageFilePath);
        var gameBoardRect = new Rectangle(540, 155, 775, 603);
        var boardBitmap = new Bitmap(gameBoardRect.Width, gameBoardRect.Height, PixelFormat.Format32bppArgb);
        using var g = Graphics.FromImage(boardBitmap);
        g.DrawImage(screenBitmap, new Rectangle(0, 0, boardBitmap.Width, boardBitmap.Height), gameBoardRect, GraphicsUnit.Pixel);

        // Arrange - Parse Expected Board
        var rawCsv = File.ReadAllLines(csvFilePath);
        var expectedTokens = rawCsv.Select(l => l.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(e => Enum.Parse<TokenType>(e)));
        var expectedBoard = new GameBoard();
        for (var y = 0; y < expectedTokens.Count(); y++)
        {
            var row = expectedTokens.ElementAt(y);
            expectedBoard.SetRow(y, row.Select(t => new Token(t)).ToArray());
        }

        // Act - Parse board image
        var tileSize = 86;
        var halfTile = tileSize / 2;
        var actualBoard = new GameBoard();
        var tokenRecognizer = new TokenRecognizer();
        for (var y = 0; y < 7; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                var tileBmp = boardBitmap.Clone(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), PixelFormat.Format32bppArgb);
                var type = tokenRecognizer.GetTokenTypeFromImage(tileBmp);
                actualBoard.SetToken(x, y, new Token(type));
            }
        }

        // Assert
        Assert.Equal(expectedBoard, actualBoard);
    }
}
