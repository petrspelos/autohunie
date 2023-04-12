namespace AutoHunie.ConsoleApp.Entities;

public class GirlInfo
{
    public string FullName { get; set; } = string.Empty;

    public Color BackgroundColor { get; set; }

    public Color BackgroundColorDark { get; set; }

    public string FavTokenImagePath { get; set; } = string.Empty;

    public string LeastFavTokenImagePath { get; set; } = string.Empty;

    public string HeadImagePath { get; set; } = string.Empty;

    public IEnumerable<GirlBaggage> Baggages { get; set; } = Array.Empty<GirlBaggage>();
}
