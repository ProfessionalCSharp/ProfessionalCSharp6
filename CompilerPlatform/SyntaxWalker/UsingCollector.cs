using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace SyntaxWalker
{
    class UsingCollector : CSharpSyntaxWalker
    {
        private readonly List<UsingDirectiveSyntax> _usingDircectives = new List<UsingDirectiveSyntax>();
        public IEnumerable<UsingDirectiveSyntax> UsingDirectives => _usingDircectives;
        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            _usingDircectives.Add(node);
        }

    }
}
