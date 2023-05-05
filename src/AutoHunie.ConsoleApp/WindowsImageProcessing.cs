using AutoHunie.Core;
using AutoHunie.Core.Entities;
using System.Drawing.Imaging;

namespace AutoHunie.ConsoleApp;

public class WindowsImageProcessing : IImageProcessing
{
    public WindowsImageProcessing()
    {
    }

    public int TileSize => 86;

    public int ExpectedBoardLocationTop => 155;

    public int ExpectedBoardLocationLeft => 540;

    public Bitmap CropToGameBoard(Bitmap screenBitmap)
    {
        var gameBoardRect = new Rectangle(ExpectedBoardLocationLeft, ExpectedBoardLocationTop, 775, 603);
        var resultBitmap = new Bitmap(gameBoardRect.Width, gameBoardRect.Height, PixelFormat.Format32bppArgb);
        using var g = Graphics.FromImage(resultBitmap);
        g.DrawImage(screenBitmap, new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), gameBoardRect, GraphicsUnit.Pixel);
        return resultBitmap;
    }

    public Bitmap GetExpectedBoardScreenshot()
    {
        var gameBoardRect = new Rectangle(ExpectedBoardLocationLeft, ExpectedBoardLocationTop, 775, 603);
        var result = new Bitmap(gameBoardRect.Width, gameBoardRect.Height, PixelFormat.Format32bppArgb);
        var g = Graphics.FromImage(result);
        g.CopyFromScreen(gameBoardRect.Left, gameBoardRect.Top, 0, 0, result.Size, CopyPixelOperation.SourceCopy);
        return result;
    }

    public (int fromX, int fromY, int toX, int toY) GetScreenCoordinatesFromMove(GameMove move)
    {
        return (ExpectedBoardLocationLeft + (move.FromX * TileSize) + (TileSize / 2),
                ExpectedBoardLocationTop + (move.FromY * TileSize) + (TileSize / 2),
                ExpectedBoardLocationLeft + (move.ToX * TileSize) + (TileSize / 2),
                ExpectedBoardLocationTop + (move.ToY * TileSize) + (TileSize / 2));
    }

    public GameBoard RecognizeBoard(Bitmap boardBitmap)
    {
        var halfTile = TileSize / 2;
        var actualBoard = new GameBoard();
        var tokenRecognizer = new TokenRecognizer();
        for (var y = 0; y < 7; y++)
        {
            for (var x = 0; x < 9; x++)
            {
                var tileBmp = boardBitmap.Clone(new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize), PixelFormat.Format32bppArgb);
                var type = tokenRecognizer.GetTokenTypeFromImage(tileBmp);
                actualBoard.SetToken(x, y, new Token(type));
            }
        }

        return actualBoard;
    }
}
