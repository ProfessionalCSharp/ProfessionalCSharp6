using Microsoft.CodeAnalysis;

namespace WPFSyntaxTree.ViewModels
{
    public class SyntaxTokenViewModel
    {
        public SyntaxTokenViewModel(SyntaxToken syntaxToken)
        {
            SyntaxToken = syntaxToken;
        }

        public SyntaxToken SyntaxToken { get; }

        public string TypeName => SyntaxToken.GetType().Name;

        public override string ToString() => SyntaxToken.ToString();
    }
}
