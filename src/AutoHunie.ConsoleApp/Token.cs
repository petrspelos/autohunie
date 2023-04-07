using System.Drawing;

namespace AutoHunie.ConsoleApp;

public class Token
{
    public Token(TokenType type)
    {
        Type = type;
    }

    public TokenType Type { get; }

    public void Draw()
    {
        switch (Type)
        {
            case TokenType.Sexuality:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[🔥]");
                break;
            case TokenType.Romance:
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("[🌙]");
                break;
            case TokenType.Joy:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[🔔]");
                break;
            case TokenType.Flirtation:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[🌟]");
                break;
            case TokenType.Sentiment:
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("[💧]");
                break;
            case TokenType.Talent:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("[🎵]");
                break;
            case TokenType.BrokenHeart:
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("[💔]");
                break;
            case TokenType.Passion:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("[💗]");
                break;
            case TokenType.Stamina:
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[⚡]");
                break;
            case TokenType.Unknown:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[❔]");
                break;
        }

        Console.ResetColor();
    }

    public static Color GetColorOf(TokenType type) => type switch
    {
        TokenType.Sexuality => Color.FromArgb(228, 83, 67),
        TokenType.Romance => Color.FromArgb(189, 106, 21),
        TokenType.Joy => Color.FromArgb(218, 179, 56),
        TokenType.Flirtation => Color.FromArgb(136, 182, 70),
        TokenType.Sentiment => Color.FromArgb(72, 186, 192),
        TokenType.Talent => Color.FromArgb(18, 91, 166),
        TokenType.BrokenHeart => Color.FromArgb(71, 11, 99),
        TokenType.Passion => Color.FromArgb(245, 81, 179),
        TokenType.Stamina => Color.FromArgb(175, 183, 203),
        _ => throw new InvalidOperationException($"No prototype color for '{type}'")
    };
}