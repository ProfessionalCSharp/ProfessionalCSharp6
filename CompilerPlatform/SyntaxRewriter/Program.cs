using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace SyntaxRewriter
{
    class Program
    {
        static void Main()
        {
            string code = File.ReadAllText("Sample.cs");
            ProcessAsync(code).Wait();
        }

        static async Task ProcessAsync(string code)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            var compilation = CSharpCompilation.Create("Sample")
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddSyntaxTrees(tree);

            SemanticModel semanticModel = compilation.GetSemanticModel(tree);

            var propertyRewriter = new AutoPropertyRewriter(semanticModel);

            SyntaxNode root = await tree.GetRootAsync().ConfigureAwait(false);
            SyntaxNode rootWithAutoProperties = propertyRewriter.Visit(root);

            compilation = compilation.RemoveAllSyntaxTrees().AddSyntaxTrees(rootWithAutoProperties.SyntaxTree);
            semanticModel = compilation.GetSemanticModel(rootWithAutoProperties.SyntaxTree);
            var fieldRewriter = new RemoveBackingFieldRewriter(semanticModel, propertyRewriter.FieldsToRemove.ToArray());
            SyntaxNode rootWithFieldsRemoved = fieldRewriter.Visit(rootWithAutoProperties);
            WriteLine(rootWithFieldsRemoved);

        }


    }
}
