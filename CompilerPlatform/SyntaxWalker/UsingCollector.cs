using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace SyntaxWalker
{
    class UsingCollector : CSharpSyntaxWalker
    {
        private readonly List<UsingDirectiveSyntax> _usingDirectives = new List<UsingDirectiveSyntax>();
        public IEnumerable<UsingDirectiveSyntax> UsingDirectives => _usingDirectives;
        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            _usingDirectives.Add(node);
        }



    }
}
