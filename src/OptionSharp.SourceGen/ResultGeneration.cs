using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace OptionSharp.SourceGen;

[Generator(LanguageNames.CSharp)]
public class ResultGeneration : IIncrementalGenerator
{
    private const string AttributeMetadataName = "OptionSharp.Result.GenerateResultAttribute";
    private const string AttributeName = "GenerateResultAttribute";
    
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var errTypesProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: AttributeMetadataName,
                predicate: static (syntaxNode, cancellationToken) => syntaxNode is RecordDeclarationSyntax,
                transform: static (syntaxContext, cancellationToken) =>
                {
                    var target = (INamedTypeSymbol)syntaxContext.TargetSymbol;

                    var attr = target
                        .GetAttributes()
                        .SingleOrDefault(attr => attr.AttributeClass?.Name == AttributeName);

                    var ctorName = attr?.ConstructorArguments[0].Value as string ?? target.Name;
                    
                    var targetNs = target.ContainingNamespace
                        .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat
                            .WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted));
                    
                    return new Model(
                        ClassName: target.Name, 
                        NamespaceName: targetNs,
                        CtorName: ctorName);
                })
            .Collect();
        
        
        context.RegisterSourceOutput(errTypesProvider, static (context, models) =>
        {
            var usings = models.Select(m => m.UsingFragment)
                .Distinct();

            var ctorClasses = models.Select(m => m.Emit);
            var sourceText = SourceText.From(
                $$"""
                   {{string.Join("\n", usings)}}

                   namespace OptionSharp;
                   
                   {{string.Join("\n", ctorClasses)}}
                   """, Encoding.UTF8);

            context.AddSource("GeneratedConstructors.g.cs", sourceText);
        });
    }

    private record Model(string ClassName, string NamespaceName, string CtorName)
    {
        public string UsingFragment => $"using {NamespaceName};";

        public string Emit =>
            $$"""
            public static class {{CtorName}}
            {
                public static Result<T, {{ClassName}}> Ok<T>(T value)
                    where T : notnull
                    => new Ok<T, {{ClassName}}>(value);
                
                public static Result<T, {{ClassName}}> Err<T>({{ClassName}} error) 
                    => new Err<T, {{ClassName}}>(error);
            }
            """;
    }
}