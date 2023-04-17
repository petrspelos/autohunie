using System.Drawing;

namespace AutoHunie.Core.Entities;

public class Token
{
    public Token(TokenType type)
    {
        Type = type;
    }

    public Token() : this (TokenType.Unknown)
    {
    }

    public TokenType Type { get; }

    public string Draw()
    {
        switch (Type)
        {
            case TokenType.Sexuality:
                Console.ForegroundColor = ConsoleColor.Red;
                //Console.Write("[SEX]");
                Console.ResetColor();
                return "[SEX]";
            case TokenType.Romance:
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                //Console.Write("[RMC]");
                Console.ResetColor();
                return "[RMC]";
            case TokenType.Joy:
                Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.Write("[JOY]");
                Console.ResetColor();
                return "[JOY]";
            case TokenType.Flirtation:
                Console.ForegroundColor = ConsoleColor.Green;
                //Console.Write("[FLR]");
                Console.ResetColor();
                return "[FLR]";
            case TokenType.Sentiment:
                Console.ForegroundColor = ConsoleColor.Cyan;
                //Console.Write("[SNT]");
                Console.ResetColor();
                return "[SNT]";
            case TokenType.Talent:
                Console.ForegroundColor = ConsoleColor.Blue;
                //Console.Write("[TLT]");
                Console.ResetColor();
                return "[TLT]";
            case TokenType.BrokenHeart:
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                //Console.Write("[BHT]");
                Console.ResetColor();
                return "[BHT]";
            case TokenType.Passion:
                Console.ForegroundColor = ConsoleColor.Magenta;
                //Console.Write("[PSN]");
                Console.ResetColor();
                return "[PSN]";
            case TokenType.Stamina:
                Console.ForegroundColor = ConsoleColor.Gray;
                //Console.Write("[STM]");
                Console.ResetColor();
                return "[STM]";
            case TokenType.Unknown:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                //Console.Write("[UNK]");
                Console.ResetColor();
                return "[UNK]";
        }

        return "?";
    }

    public override string ToString()
    {
        return Draw();
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

    public static bool operator ==(Token obj1, Token obj2)
    {
        if (obj1 is null)
            return obj2 is null;
        
        return obj1.Equals(obj2);
    }

    public static bool operator!= (Token obj1, Token obj2)
    {
        if (obj1 is null)
            return obj2 is not null;
        
        return !obj1.Equals(obj2);
    }

    public override bool Equals(object? obj) => Equals(obj as Token);

    public bool Equals(Token? other) => other is not null && other.Type == this.Type;

    public override int GetHashCode() => HashCode.Combine(this.Type);
}
