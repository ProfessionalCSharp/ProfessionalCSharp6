using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;


namespace SyntaxWalker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            string path = args[0];
            if (!Directory.Exists(path))
            {
                ShowUsage();
                return;
            }

            ProcessUsingsAsync(path).Wait();
        }

        static void ShowUsage()
        {
            WriteLine("Usage: SyntaxWalker directory");
        }

        static async Task ProcessUsingsAsync(string path)
        {
            const string searchPattern = "*.cs";
            var collector = new UsingCollector();

            IEnumerable<string> fileNames = Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories).Where(fileName => !fileName.EndsWith(".g.i.cs") && !fileName.EndsWith(".g.cs"));
            foreach (var fileName in fileNames)
            {
                string code = File.ReadAllText(fileName);
                SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
                SyntaxNode root = await tree.GetRootAsync();
                collector.Visit(root);
            }

            var usings = collector.UsingDirectives;
            var usingStatics = usings.Select(n => n.ToString()).Distinct().Where(u => u.StartsWith("using static")).OrderBy(u => u);
            var orderedUsings = usings.Select(n => n.ToString()).Distinct().Except(usingStatics).OrderBy(u => u.Substring(0, u.Length - 1));
            foreach (var item in orderedUsings.Union(usingStatics))
            {
                WriteLine(item);
            }
        }


    }
}
