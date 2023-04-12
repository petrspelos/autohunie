using System.Drawing;

namespace AutoHunie.Core;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
internal static class BitmapExtensions
{
    internal static int TileSize = 86;

    internal static void DrawStraightLine(this Bitmap bitmap, int fromX, int fromY, int toX, int toY, Color color, int thickness = 1)
    {
        if (fromX != toX && fromY != toY)
            throw new InvalidOperationException($"Cannot draw a straight line for coordinates: x:{fromX} y:{fromY} -> x:{toX} y:{toY}");

        if (thickness < 1)
            throw new InvalidOperationException($"Cannot draw a line of thickness {thickness}");

        var xAxis = fromX != toX;
        var from = xAxis ? fromX : fromY;
        var to = xAxis ? toX : toY;

        if (from > to)
        {
            var temp = from;
            from = to;
            to = temp;
        }

        for (var thicknessMod = 0; thicknessMod < thickness; thicknessMod++)
        {
            for (var i = from; i < to; i++)
            {
                var x = xAxis ? i : fromX + thicknessMod;
                var y = xAxis ? fromY + thicknessMod : i;

                if (bitmap.IsWithinBounds(x, y))
                    bitmap.SetPixel(x, y, color);
            }
        }
    }


    internal static bool IsWithinBounds(this Bitmap bitmap, int x, int y)
        => x >= 0 && y >= 0 && y < bitmap.Height && x < bitmap.Width;
}
