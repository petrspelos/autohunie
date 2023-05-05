using AutoHunie.Core.Entities;
using System.Drawing;

namespace AutoHunie.Core;

public interface IImageProcessing
{
    /// <summary>
    /// Returns a subsection of the provided bitmap.
    /// The subsection contains the board.
    /// </summary>
    /// <param name="screenBitmap">Bitmap of the current full screen to crop from</param>
    /// <returns>A Bitmap containing the subsection of the screen where game board is expected</returns>
    Bitmap CropToGameBoard(Bitmap screenBitmap);

    /// <summary>
    /// Recognizes a board bitmap and returns the displayed GameBoard representation
    /// </summary>
    /// <param name="boardBitmap">board bitmap to recognize, expected to be cropped</param>
    /// <returns>GameBoard object of the represented board</returns>
    GameBoard RecognizeBoard(Bitmap boardBitmap);
}
