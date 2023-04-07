using System.Text.RegularExpressions;

namespace AutoHunie.ConsoleApp;

public static class Logo
{
    public static void Draw()
    {
        var logoMarkup = File.ReadAllText("logo.lgo");

        var codeRegex = new Regex(@"\[(\d+)\]", RegexOptions.Compiled);

        for (var i = 0; i < logoMarkup.Length; i++)
        {
            if (logoMarkup[i] == '[')
            {
                var match = codeRegex.Match(logoMarkup.Substring(i));
                var capture = match.Groups.Values.ElementAtOrDefault(1);

                if (capture is null)
                {
                    continue;
                }

                Console.ForegroundColor = (ConsoleColor)int.Parse(capture.Value);
                i += capture.Value.Length + 2;
            }

            Console.Write(logoMarkup[i]);
        }
    }
}
