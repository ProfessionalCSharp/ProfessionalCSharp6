using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace SyntaxQuery
{
    class Program
    {
        static void Main()
        {

            CheckLowercaseMembers().Wait();

        }

        static async Task CheckLowercaseMembers()
        {
            string code = File.ReadAllText("../../Program.cs");
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            SyntaxNode root = await tree.GetRootAsync();
            
            var methods = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(m => char.IsLower(m.Identifier.ValueText.First()))
                .Where(m => m.Modifiers.Select(t => t.Value).Contains("public"));
            WriteLine("Public methods with lowercase first character:");
            foreach (var m in methods)
            {
                WriteLine(m.Identifier.ValueText);             
            }

            var properties = root.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(p => char.IsLower(p.Identifier.ValueText.First()))
                .Where(p => p.Modifiers.Select(t => t.Value).Contains("public"));

            WriteLine("Public properties with lowercase first character:");
            foreach (var p in properties)
            {
                WriteLine(p.Identifier.ValueText);
            }
        }

        public void foo()
        {

        }

        private void foobar()
        {

        }

        public int bar { get; set; }



    }
}
