using Microsoft.CodeAnalysis;

namespace WPFSyntaxTree.ViewModels
{
    public enum TriviaKind
    {
        Leading,
        Trailing,
        Structured,
        Annotated
    }

    public class SyntaxTriviaViewModel
    {
        public SyntaxTriviaViewModel(TriviaKind kind, SyntaxTrivia syntaxTrivia)
        {
            TriviaKind = kind;
            SyntaxTrivia = syntaxTrivia;
        }

        public SyntaxTrivia SyntaxTrivia { get; }
        public TriviaKind TriviaKind { get; }

        public override string ToString() => $"{TriviaKind}, Start: {SyntaxTrivia.Span.Start}, Length: {SyntaxTrivia.Span.Length} : {SyntaxTrivia}";

    }
}
