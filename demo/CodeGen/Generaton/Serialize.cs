using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Text;

namespace Generaton
{
    [Generator]
    public class Serialize : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var classes = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => IsSyntaxTarget(node),
                transform: static (ctx, _) => GetSemanticTarget(ctx))
                .Where(static (target) => target is not null);

            context.RegisterSourceOutput(classes,
                Execute);

            context.RegisterPostInitializationOutput(PostInit);
        }

        private static bool IsSyntaxTarget(SyntaxNode node)
        {
            return node is ClassDeclarationSyntax classDeclarationSyntax
                && classDeclarationSyntax.AttributeLists.Count > 0;
        }

        private static ClassDeclarationSyntax? GetSemanticTarget(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

            foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in attributeListSyntax.Attributes)
                {
                    var attributeName = attributeSyntax.Name.ToString();

                    if (attributeName.Contains("Serialize"))
                    {
                        return classDeclarationSyntax;
                    }
                }
            }

            return null;
        }

        // Generate the SerializeAttribute
        private static void PostInit(IncrementalGeneratorPostInitializationContext context)
        {
            context.AddSource("Generaton.SerializeAttribute.g.cs", @"namespace Generaton;

public class SerializeAttribute : System.Attribute { }");
        }

        private static void Execute(SourceProductionContext ctx, ClassDeclarationSyntax source)
        {
            if (source.Parent is not BaseNamespaceDeclarationSyntax ns)
            {
                return;
            }

            var namespaceName = ns.Name.ToString();
            var className = source.Identifier.Text;

            var fileName = $"{namespaceName}.{className}.g.cs";
            var publicProperties = source.Members
                .Where(x => x is PropertyDeclarationSyntax pds && pds.Modifiers.Any(SyntaxKind.PublicKeyword));

            var sb = new StringBuilder()
                .AppendLine($"namespace {namespaceName};")
                .AppendLine($"using System.Text.Json;")
                .Append($@"partial class {className}
{{
    public override string ToString()
    {{
        return JsonSerializer.Serialize(this);
    }}
}}");

            ctx.AddSource(fileName, sb.ToString());
        }
    }
}
