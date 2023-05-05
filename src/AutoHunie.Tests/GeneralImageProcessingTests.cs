using AutoHunie.ConsoleApp;
using AutoHunie.Core;
using AutoHunie.Core.Entities;

namespace AutoHunie.Tests;

public class GeneralImageProcessingTests
{
    private readonly IImageProcessing _imageProcessing;

    public GeneralImageProcessingTests()
    {
        _imageProcessing = new WindowsImageProcessing();
    }

    [Fact]
    public void GetScreenCoordinatesFromMove_GivenValidData_ShouldReturnScreenCoordinates()
    {
        var expected = new {
            fromX = _imageProcessing.ExpectedBoardLocationLeft + _imageProcessing.TileSize + (_imageProcessing.TileSize / 2),
            fromY = _imageProcessing.ExpectedBoardLocationTop + (_imageProcessing.TileSize * 2) + (_imageProcessing.TileSize / 2),
            toX = _imageProcessing.ExpectedBoardLocationLeft + (_imageProcessing.TileSize * 3) + (_imageProcessing.TileSize / 2),
            toY = _imageProcessing.ExpectedBoardLocationTop + (_imageProcessing.TileSize * 4) + (_imageProcessing.TileSize / 2)
        };
        var move = new GameMove(1, 2, 3, 4);

        var actual = _imageProcessing.GetScreenCoordinatesFromMove(move);

        Assert.Equal(expected.fromX, actual.fromX);
        Assert.Equal(expected.fromY, actual.fromY);
        Assert.Equal(expected.toX, actual.toX);
        Assert.Equal(expected.toY, actual.toY);
    }
}
