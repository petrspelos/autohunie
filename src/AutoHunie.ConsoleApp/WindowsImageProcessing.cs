using AutoHunie.Core;
using AutoHunie.Core.Entities;
using System.Drawing.Imaging;

namespace AutoHunie.ConsoleApp;

public class WindowsImageProcessing : IImageProcessing
{
    public Bitmap CropToGameBoard(Bitmap screenBitmap)
    {
        var gameBoardRect = new Rectangle(540, 155, 775, 603);
        var resultBitmap = new Bitmap(gameBoardRect.Width, gameBoardRect.Height, PixelFormat.Format32bppArgb);
        using var g = Graphics.FromImage(resultBitmap);
        g.DrawImage(screenBitmap, new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), gameBoardRect, GraphicsUnit.Pixel);
        return resultBitmap;
    }

    public GameBoard RecognizeBoard(Bitmap boardBitmap)
    {
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

        return actualBoard;
    }
}
