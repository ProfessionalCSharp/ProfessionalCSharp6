using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Simplification;
using System.Linq;

namespace PropertyCodeRefactoring
{
    internal static class CodeGeneration
    {
        private static SyntaxAnnotation UpdatedPropertyAnnotation = new SyntaxAnnotation("UpdatedProperty");

        internal static CompilationUnitSyntax ImplementFullProperty(CompilationUnitSyntax root, SemanticModel model, PropertyDeclarationSyntax propertyDecl, Workspace workspace)
        {
            TypeDeclarationSyntax typeDecl = propertyDecl.FirstAncestorOrSelf<TypeDeclarationSyntax>();
            string propertyName = propertyDecl.Identifier.ValueText;
            string backingFieldName = $"_{char.ToLower(propertyName[0])}{propertyName.Substring(1)}";
            ITypeSymbol propertyTypeSymbol = model.GetDeclaredSymbol(propertyDecl).Type;

            root = root.ReplaceNodes(new SyntaxNode[] { propertyDecl, typeDecl },
                (original, updated) =>
                original.IsKind(SyntaxKind.PropertyDeclaration)
                ? ExpandProperty((PropertyDeclarationSyntax)original, (PropertyDeclarationSyntax)updated, backingFieldName) as SyntaxNode
                : ExpandType((TypeDeclarationSyntax)original, (TypeDeclarationSyntax)updated, propertyTypeSymbol, backingFieldName, model, workspace) as SyntaxNode
            );

            return root;
        }

        private static TypeDeclarationSyntax ExpandType(TypeDeclarationSyntax original, TypeDeclarationSyntax updated, ITypeSymbol typeSymbol, string backingFieldName, SemanticModel model, Workspace workspace)
        {
            return updated.WithBackingField(typeSymbol, backingFieldName, model, workspace);
        }

        private static TypeDeclarationSyntax WithBackingField(this TypeDeclarationSyntax node, ITypeSymbol typeSymbol, string backingFieldName, SemanticModel model, Workspace workspace)
        {
            PropertyDeclarationSyntax property = node.ChildNodes().Where(n => n.HasAnnotation(UpdatedPropertyAnnotation)).FirstOrDefault() as PropertyDeclarationSyntax;
            if (property == null)
            {
                return null;
            }

            MemberDeclarationSyntax fieldDecl = GenerateBackingField(typeSymbol, backingFieldName, workspace);
            node = node.InsertNodesBefore(property, new[] { fieldDecl });
            return node;

        }

        private static MemberDeclarationSyntax GenerateBackingField(ITypeSymbol typeSymbol, string backingFieldName, Workspace workspace)
        {
            var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
            SyntaxNode type = generator.TypeExpression(typeSymbol);
            FieldDeclarationSyntax fieldDecl = ParseMember($"private _field_Type_ {backingFieldName};") as FieldDeclarationSyntax;
            return fieldDecl.ReplaceNode(fieldDecl.Declaration.Type, type.WithAdditionalAnnotations(Simplifier.SpecialTypeAnnotation)); 
        }

        private static MemberDeclarationSyntax ParseMember(string member)
        {
            MemberDeclarationSyntax decl = (SyntaxFactory.ParseCompilationUnit($"class x {{\r\n{member}\r\n}}").Members[0] as ClassDeclarationSyntax).Members[0];
            return decl.WithAdditionalAnnotations(Formatter.Annotation);
        }


        private static PropertyDeclarationSyntax ExpandProperty(PropertyDeclarationSyntax original, PropertyDeclarationSyntax updated, string backingFieldName)
        {

            AccessorDeclarationSyntax getter = original.AccessorList.Accessors.FirstOrDefault(ad => ad.Kind() == SyntaxKind.GetAccessorDeclaration);
            var returnFieldStatement = SyntaxFactory.ParseStatement($"return {backingFieldName};");
            getter = getter
                .WithBody(SyntaxFactory.Block(SyntaxFactory.SingletonList(returnFieldStatement)))
                .WithSemicolonToken(default(SyntaxToken));

            AccessorDeclarationSyntax setter = original.AccessorList.Accessors.FirstOrDefault(ad => ad.Kind() == SyntaxKind.SetAccessorDeclaration);

            var setPropertyStatement = SyntaxFactory.ParseStatement($"{backingFieldName} = value;");
            setter = setter
                .WithBody(SyntaxFactory.Block(SyntaxFactory.SingletonList(setPropertyStatement)))
                .WithSemicolonToken(default(SyntaxToken));

            updated = updated
                .WithAccessorList(SyntaxFactory.AccessorList(SyntaxFactory.List(new[] { getter, setter })))
                .WithAdditionalAnnotations(Formatter.Annotation)
                .WithAdditionalAnnotations(UpdatedPropertyAnnotation);
            return updated;
        }
    }
}
