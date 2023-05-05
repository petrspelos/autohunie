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

    public static IEnumerable<GirlInfo> GetAll()
    {
        return new GirlInfo[]
        {
            new()
            {
                FullName = "Abia Nawazi",
                BackgroundColor = Color.Crimson,
                BackgroundColorDark = Color.Firebrick,
                HeadImagePath = @"resources\Abia head.png",
                FavTokenImagePath = @"resources\sexuality token trans.png",
                LeastFavTokenImagePath = @"resources\talent token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Sex Addict", IconPath = @"resources\Item_baggage_sex_addict.png" },
                    new() { Name = "One Pump Chump", IconPath = @"resources\Item_baggage_one_pump_chump.png" },
                    new() { Name = "Self-Effacing", IconPath = @"resources\Item_baggage_self_effacing.png" }
                }
            },
            new()
            {
                FullName = "Ashley Rosemarry",
                BackgroundColor = Color.Crimson,
                BackgroundColorDark = Color.Firebrick,
                HeadImagePath = @"resources\Ashley head.png",
                FavTokenImagePath = @"resources\sexuality token trans.png",
                LeastFavTokenImagePath = @"resources\romance token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Allergies", IconPath = @"resources\Item_baggage_allergies.png" },
                    new() { Name = "Commitment Issues", IconPath = @"resources\Item_baggage_commitment_issues.png" },
                    new() { Name = "Easily Bored", IconPath = @"resources\Item_baggage_easily_bored.png" }
                }
            },
            new()
            {
                FullName = "Brooke Belrose",
                BackgroundColor = Color.DodgerBlue,
                BackgroundColorDark = Color.FromArgb(0, 102, 204),
                HeadImagePath = @"resources\Brooke head.png",
                FavTokenImagePath = @"resources\talent token trans.png",
                LeastFavTokenImagePath = @"resources\romance token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Expensive Tastes", IconPath = @"resources\Item_baggage_expensive_tastes.png" },
                    new() { Name = "Unsentimental", IconPath = @"resources\Item_baggage_unsentimental.png" },
                    new() { Name = "Gold Digger", IconPath = @"resources\Item_baggage_gold_digger.png" }
                }
            },
            new()
            {
                FullName = "Candace Crush",
                BackgroundColor = Color.LimeGreen,
                BackgroundColorDark = Color.ForestGreen,
                HeadImagePath = @"resources\Candace head.png",
                FavTokenImagePath = @"resources\flirtation token trans.png",
                LeastFavTokenImagePath = @"resources\romance token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Intellectually Challenged", IconPath = @"resources\Item_baggage_intellectually_challenged.png" },
                    new() { Name = "Forgetful", IconPath = @"resources\Item_baggage_forgetful.png" },
                    new() { Name = "Hypersensitive", IconPath = @"resources\Item_baggage_hypersensitive.png" }
                }
            },
            new()
            {
                FullName = "Jessie Maye",
                BackgroundColor = Color.Orange,
                BackgroundColorDark = Color.FromArgb(255, 128, 0),
                HeadImagePath = @"resources\Jessie head.png",
                FavTokenImagePath = @"resources\romance token trans.png",
                LeastFavTokenImagePath = @"resources\talent token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Depression", IconPath = @"resources\Item_baggage_depression.png" },
                    new() { Name = "Emphysema", IconPath = @"resources\Item_baggage_emphysema.png" },
                    new() { Name = "Busted Vadge", IconPath = @"resources\Item_baggage_busted_vadge.png" }
                }
            },
            new()
            {
                FullName = "Lailani Kealoha",
                BackgroundColor = Color.Orange,
                BackgroundColorDark = Color.FromArgb(255, 128, 0),
                HeadImagePath = @"resources\Lailani head.png",
                FavTokenImagePath = @"resources\romance token trans.png",
                LeastFavTokenImagePath = @"resources\sexuality token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Old Fashioned", IconPath = @"resources\Item_baggage_old_fashioned.png" },
                    new() { Name = "Low Self-Esteem", IconPath = @"resources\Item_baggage_low_self_esteem.png" },
                    new() { Name = "Sheepish", IconPath = @"resources\Item_baggage_sheepish.png" }
                }
            },
            new()
            {
                FullName = "Lillian Aurawell",
                BackgroundColor = Color.Crimson,
                BackgroundColorDark = Color.Firebrick,
                HeadImagePath = @"resources\Lillian head.png",
                FavTokenImagePath = @"resources\sexuality token trans.png",
                LeastFavTokenImagePath = @"resources\flirtation token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "The Darkness", IconPath = @"resources\Item_baggage_the_darkness.png" },
                    new() { Name = "Asthma", IconPath = @"resources\Item_baggage_asthma.png" },
                    new() { Name = "Teen Angst", IconPath = @"resources\Item_baggage_teen_angst.png" }
                }
            },
            new()
            {
                FullName = "Lola Rembrite",
                BackgroundColor = Color.DodgerBlue,
                BackgroundColorDark = Color.FromArgb(0, 102, 204),
                HeadImagePath = @"resources\Lola head.png",
                FavTokenImagePath = @"resources\talent token trans.png",
                LeastFavTokenImagePath = @"resources\flirtation token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Busy Schedule", IconPath = @"resources\Item_baggage_busy_schedule.png" },
                    new() { Name = "Caffeine Junkie", IconPath = @"resources\Item_baggage_caffeine_junkie.png" },
                    new() { Name = "Miss Independent", IconPath = @"resources\Item_baggage_miss_independent.png" }
                }
            },
            new()
            {
                FullName = "Nora Delrio",
                BackgroundColor = Color.Orange,
                BackgroundColorDark = Color.FromArgb(255, 128, 0),
                HeadImagePath = @"resources\Nora head.png",
                FavTokenImagePath = @"resources\romance token trans.png",
                LeastFavTokenImagePath = @"resources\flirtation token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Abandonment Issues", IconPath = @"resources\Item_baggage_abandonment_issues.png" },
                    new() { Name = "Emotionally Guarded", IconPath = @"resources\Item_baggage_emotionally_guarded.png" },
                    new() { Name = "Vindictive", IconPath = @"resources\Item_baggage_vindictive.png" }
                }
            },
            new()
            {
                FullName = "Polly Bendelson",
                BackgroundColor = Color.LimeGreen,
                BackgroundColorDark = Color.ForestGreen,
                HeadImagePath = @"resources\Polly head.png",
                FavTokenImagePath = @"resources\flirtation token trans.png",
                LeastFavTokenImagePath = @"resources\talent token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Drama Queen", IconPath = @"resources\Item_baggage_drama_queen.png" },
                    new() { Name = "Jealousy", IconPath = @"resources\Item_baggage_jealousy.png" },
                    new() { Name = "Brand Loyalist", IconPath = @"resources\Item_baggage_brand_loyalist.png" }
                }
            },
            new()
            {
                FullName = "Sarah Stevens",
                BackgroundColor = Color.LimeGreen,
                BackgroundColorDark = Color.ForestGreen,
                HeadImagePath = @"resources\Sarah head.png",
                FavTokenImagePath = @"resources\flirtation token trans.png",
                LeastFavTokenImagePath = @"resources\sexuality token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Annoying as Fuck", IconPath = @"resources\Item_baggage_annoying_as_fuck.png" },
                    new() { Name = "Attention Whore", IconPath = @"resources\Item_baggage_attention_whore.png" },
                    new() { Name = "Smelly Pussy", IconPath = @"resources\Item_baggage_smelly_pussy.png" }
                }
            },
            new()
            {
                FullName = "Zoey Greene",
                BackgroundColor = Color.DodgerBlue,
                BackgroundColorDark = Color.FromArgb(0, 102, 204),
                HeadImagePath = @"resources\Zoey head.png",
                FavTokenImagePath = @"resources\talent token trans.png",
                LeastFavTokenImagePath = @"resources\sexuality token trans.png",
                Baggages = new GirlBaggage[]
                {
                    new() { Name = "Kinda Crazy", IconPath = @"resources\Item_baggage_kinda_crazy.png" },
                    new() { Name = "Aquaphobic", IconPath = @"resources\Item_baggage_aquaphobic.png" },
                    new() { Name = "Tinnitus", IconPath = @"resources\Item_baggage_tinnitus.png" }
                }
            }
        };
    }
}
