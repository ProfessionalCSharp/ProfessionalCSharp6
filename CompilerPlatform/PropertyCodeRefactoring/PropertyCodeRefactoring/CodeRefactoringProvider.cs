using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PropertyCodeRefactoring
{
    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(PropertyCodeRefactoringProvider)), Shared]
    internal class PropertyCodeRefactoringProvider : CodeRefactoringProvider
    {
        public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            SyntaxNode root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            SyntaxNode selectedNode = root.FindNode(context.Span);
            var propertyDecl = selectedNode as PropertyDeclarationSyntax;
            if (propertyDecl == null || !IsAutoImplementedProperty(propertyDecl))
            {
                return;
            }

            // For any type declaration node, create a code action to reverse the identifier text.
            var action = CodeAction.Create("Apply full property", cancellationToken => ChangeToFullPropertyAsync(context.Document, propertyDecl, cancellationToken));

            //// Register this code action.
            context.RegisterRefactoring(action);
        }

        private bool IsAutoImplementedProperty(PropertyDeclarationSyntax propertyDecl)
        {
            SyntaxList<AccessorDeclarationSyntax> accessors = propertyDecl.AccessorList.Accessors;

            AccessorDeclarationSyntax getter = accessors.FirstOrDefault(ad => ad.Kind() == SyntaxKind.GetAccessorDeclaration);
            AccessorDeclarationSyntax setter = accessors.FirstOrDefault(ad => ad.Kind() == SyntaxKind.SetAccessorDeclaration);
            if (getter == null || setter == null) return false;
            return getter.Body == null && setter.Body == null;
        }


        private async Task<Document> ChangeToFullPropertyAsync(Document document, PropertyDeclarationSyntax propertyDecl, CancellationToken cancellationToken)
        {

            SemanticModel model = await document.GetSemanticModelAsync(cancellationToken);
            var root = await document.GetSyntaxRootAsync(cancellationToken) as CompilationUnitSyntax;

            document = document.WithSyntaxRoot(CodeGeneration.ImplementFullProperty(root, model, propertyDecl, document.Project.Solution.Workspace));
            return document;
        }
    }

}