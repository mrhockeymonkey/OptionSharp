using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using OptionSharp.Result;

namespace OptionSharp.SourceGen.Tests;

public static class TestHelper
{
    public static Task Verify(string source)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        // Create a Roslyn compilation for the syntax tree.
        var references = new[] {
            // Resolves to System.Private.CoreLib.dll
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            // Resolves to System.Runtime.dll, which is needed for the Attribute type
            // Can't use typeof(Attribute).GetTypeInfo().Assembly.Location because it resolves to System.Private.CoreLib.dll
            MetadataReference.CreateFromFile(
                AppDomain
                    .CurrentDomain.GetAssemblies()
                    .First(static assembly =>
                        assembly.FullName?.Contains("System.Runtime") is true
                    )
                    .Location
            ),
            MetadataReference.CreateFromFile(typeof(GenerateResultAttribute).Assembly.Location), 
        };
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "compilation",
            syntaxTrees: [syntaxTree],
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


        // Create an instance of our EnumGenerator incremental source generator
        var generator = new ResultGeneration();

        // The GeneratorDriver is used to run our generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the source generator!
        driver = driver.RunGenerators(compilation);

        // Use verify to snapshot test the source generator output!
        return Verifier.Verify(driver);
    }

}