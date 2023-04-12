using System.Drawing;
using System.Text;
using AutoHunie.Core;
using AutoHunie.Core.Entities;

namespace AutoHunie.ConsoleApp;

public class TokenRecognizer
{
    const int TileSize = 86;
    const int TokenCenter = TileSize / 2;

    public TokenType GetTokenTypeFromImage(Bitmap image)
    {
        var coords = new List<Tuple<int, int>>();

        for (var i = TokenCenter - 20; i < TokenCenter + 20; i++)
        {
            for (var j = TokenCenter - 20; j < TokenCenter + 20; j++)
            {
                coords.Add(new (i, j));
            }
        }

        var centerColors = coords.Select(c => image.GetPixel(c.Item1, c.Item2));

        var color = image.GetPixel(TokenCenter, TokenCenter);
        
        for (var i = 0; i < TileSize; i++)
        {
            for (var j = 0; j < 40; j++)
            {
                image.SetPixel(i, j, color);
            }
        }

        for (var i = TokenCenter - 20; i < TokenCenter + 20; i++)
        {
            image.SetPixel(26, i, Color.Red);
            image.SetPixel(48, i, Color.Red);
            image.SetPixel(i, 26, Color.Red);
            image.SetPixel(i, 48, Color.Red);
        }

        var candidates = new TokenType?[]
        {
            TokenType.Sexuality,
            TokenType.Romance,
            TokenType.Joy,
            TokenType.Flirtation,
            TokenType.Sentiment,
            TokenType.Talent,
            TokenType.BrokenHeart,
            TokenType.Passion,
            TokenType.Stamina
        };

        var bestCandidate = candidates.FirstOrDefault(c => centerColors.Any(cc => GetColorDifferenceScore(cc, Token.GetColorOf(c ?? TokenType.Unknown)) < 3));

        if (bestCandidate is null)
        {
            image.Save("Unreadable.png");
            var sb = new StringBuilder();
            foreach (var centerColor in centerColors)
            {
                sb.AppendLine($"R:{centerColor.R} G:{centerColor.G} B:{centerColor.B}");
            }
            File.WriteAllText("centerColors.txt", sb.ToString());
            throw new InvalidOperationException($"Unfortunately, there was no color match in the image 😭");
        }

        return bestCandidate.Value;
    }

    public int GetColorDifferenceScore(Color a, Color b)
    {
        var diffR = Math.Abs(a.R - b.R);
        var diffG = Math.Abs(a.G - b.G);
        var diffB = Math.Abs(a.B - b.B);

        return diffR + diffG + diffB;
    }

    public int GetColorBrightnessScore(Color color) => color.R + color.G + color.B;

}
