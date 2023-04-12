namespace AutoHunie.Core.Entities;

public class GameMove
{
    const int TileSize = 86;

    const int HalfTileSize = TileSize / 2;

    public int FromX { get; set; }
    public int FromY { get; set; }
    public int ToX { get; set; }
    public int ToY { get; set; }

    public int FromScreenX => GetTileized(FromX);
    public int FromScreenY => GetTileized(FromY);
    public int ToScreenX => GetTileized(ToX);
    public int ToScreenY => GetTileized(ToY);

    public GameMove(int fromX, int fromY, int toX, int toY)
    {
        FromX = fromX;
        FromY = fromY;
        ToX = toX;
        ToY = toY;
    }

    public int GetTileized(int x) => (x * TileSize) + HalfTileSize;
}
