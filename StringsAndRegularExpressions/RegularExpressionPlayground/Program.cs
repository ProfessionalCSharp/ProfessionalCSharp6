using System.Text.RegularExpressions;
using static System.Console;

namespace RegularExpressionPlayground
{
    class Program
    {
        const string text =
            @"This book is perfect for both experienced C# programmers " +
            "looking to sharpen their skills and professional developers who are using C# for " +
            "the first time. The authors deliver unparalleled coverage of Visual Studio 2013 " +
            "and .NET Framework 4.5.1 additions, as well as new test-driven development and " +
            "concurrent programming features. Source code for all the examples are available " +
            "for download, so you can start writing Windows desktop, Windows Store apps, and " +
            "ASP.NET web applications immediately.";


        static void Main()
        {
            Find1(text);
            Find2(text);
            Groups();
            NamedGroups();
            ReadLine();
        }

        public static void NamedGroups()
        {
            WriteLine("NamedGroups\n");
            string line = "Hey, I've just found this amazing URI at http:// what was it --oh yes https://www.wrox.com or http://www.wrox.com:80";

            string pattern = @"\b(?<protocol>https?)(?:://)(?<address>[.\w]+)([\s:](?<port>[\d]{2,4})?)\b";
            Regex r = new Regex(pattern, RegexOptions.ExplicitCapture);

            MatchCollection mc = r.Matches(line);
            foreach (Match m in mc)
            {
                WriteLine($"match: {m} at {m.Index}");

                foreach (var groupName in r.GetGroupNames())
                {
                    WriteLine($"match for {groupName}: {m.Groups[groupName].Value}");
                }
                WriteLine();
            }
            WriteLine();

        }

        public static void Groups()
        {
            WriteLine("Groups\n");
            string line = "Hey, I've just found this amazing URI at http:// what was it --oh yes https://www.wrox.com or http://www.wrox.com:80";

            string pattern = @"\b(https?)(://)([.\w]+)([\s:]([\d]{2,4})?)\b";
            var r = new Regex(pattern);
            MatchCollection mc = r.Matches(line);            
            
            foreach (Match m in mc)
            {

                WriteLine($"Match: {m}\n");
                foreach (Group g in m.Groups)
                {
                    if (g.Success)
                    {
                        WriteLine($"group index: {g.Index}, value: {g.Value}");
                    }
                }
                WriteLine();             
            }
            WriteLine();
        }

        public static void Find1(string text)
        {
            WriteLine("Find1\n");
            const string pattern = "ion";

            MatchCollection matches = Regex.Matches(text, pattern,
                                                    RegexOptions.IgnoreCase |
                                                    RegexOptions.ExplicitCapture);
            WriteMatches(text, matches);
            WriteLine();
        }

        public static void WriteMatches(string text, MatchCollection matches)
        {
            WriteLine($"Original text was: \n\n{text}\n");
            WriteLine($"No. of matches: {matches.Count}");

            foreach (Match nextMatch in matches)
            {
                int index = nextMatch.Index;
                string result = nextMatch.ToString();
                int charsBefore = (index < 5) ? index : 5;
                int fromEnd = text.Length - index - result.Length;
                int charsAfter = (fromEnd < 5) ? fromEnd : 5;
                int charsToDisplay = charsBefore + charsAfter + result.Length;

                WriteLine($"Index: {index}, \tString: {result}, \t" +
                  $"{text.Substring(index - charsBefore, charsToDisplay)}");
            }
        }

        public static void Find2(string text)
        {
            WriteLine("Find2\n");
            const string pattern = @"\ba\S*ions\b";
            MatchCollection matches = Regex.Matches(text, pattern,
                RegexOptions.IgnoreCase);
            WriteMatches(text, matches);
            WriteLine();
        }
    }
}
