using System.Text.RegularExpressions;

Console.Clear();
var logoMarkup = File.ReadAllText("logo.lgo");



var codeRegex = new Regex(@"\[(\d+)\]", RegexOptions.Compiled);

for (var i = 0; i < logoMarkup.Length; i++) {    
    if (logoMarkup[i] == '[') {
        var match = codeRegex.Match(logoMarkup.Substring(i));
        var capture = match.Groups.Values.ElementAtOrDefault(1);

        if (capture is null) {
            continue;
        }

        Console.ForegroundColor = (ConsoleColor)int.Parse(capture.Value);
        i += capture.Value.Length + 2;
    }
    
    Console.Write(logoMarkup[i]);
}



foreach (ConsoleColor c in (ConsoleColor[]) Enum.GetValues(typeof(ConsoleColor)))
{
    Console.ForegroundColor = c;
    Console.WriteLine($"[{(int)c}] Hello, HuniePop2! [{c}]");
}
