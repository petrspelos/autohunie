using System.Drawing;

namespace AutoHunie.Core.Entities;

public record Token
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
                return "[SEX]";
            case TokenType.Romance:
                return "[RMC]";
            case TokenType.Joy:
                return "[JOY]";
            case TokenType.Flirtation:
                return "[FLR]";
            case TokenType.Sentiment:
                return "[SNT]";
            case TokenType.Talent:
                return "[TLT]";
            case TokenType.BrokenHeart:
                return "[BHT]";
            case TokenType.Passion:
                return "[PSN]";
            case TokenType.Stamina:
                return "[STM]";
            case TokenType.Unknown:
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
}
