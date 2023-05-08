namespace AutoHunie.Core;

public static class TokenWeights
{
    public static int Stamina { get; set; } = 5;
    public static int Sentiment { get; set; } = 5;
    public static int Joy { get; set; } = 5;
    public static int Passion { get; set; } = 10;
    public static int Talent { get; set; } = 10;
    public static int Sexuality { get; set; } = 10;
    public static int Romance { get; set; } = 10;
    public static int Flirtation { get; set; } = 5;
    public static int BrokenHeart { get; set; } = -20;
    public static int QuadMultiplier { get; set; } = 100;
}
