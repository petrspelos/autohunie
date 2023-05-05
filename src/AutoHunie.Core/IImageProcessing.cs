using AutoHunie.Core.Entities;
using System.Drawing;

namespace AutoHunie.Core;

public interface IImageProcessing
{
    /// <summary>
    /// The size of a token tile in pixels
    /// </summary>
    int TileSize { get; }

    /// <summary>
    /// The expected location of the game board from the top of the screen
    /// In other words: Expected margin top
    /// </summary>
    int ExpectedBoardLocationTop { get; }

    /// <summary>
    /// The expected location of the game board from the left of the screen
    /// In other words: Expected margin left
    /// </summary>
    int ExpectedBoardLocationLeft { get; }

    /// <summary>
    /// Returns a subsection of the screen where the game board is expected
    /// Screenshot of the full screen is taken and the subsection is returned as a bitmap
    /// </summary>
    /// <returns></returns>
    Bitmap GetExpectedBoardScreenshot();

    /// <summary>
    /// Returns a subsection of the provided bitmap
    /// The subsection contains the board
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

    /// <summary>
    /// Translates game board coordinates into screen coordinates
    /// These can be directly used to simulate mouse movement for automation
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    (int fromX, int fromY, int toX, int toY) GetScreenCoordinatesFromMove(GameMove move);
}
