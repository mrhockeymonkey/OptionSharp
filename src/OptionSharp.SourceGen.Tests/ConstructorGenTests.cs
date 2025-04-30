namespace OptionSharp.SourceGen.Tests;

public class ConstructorGenTests
{
    [Fact]
    public Task GivenAttributePresentWithNameArg_ThenGenerateWithName()
    {
        var source = """
                     using OptionSharp.Result;
                     
                     namespace MyApp.Errors;
                     
                     [GenerateResult("Test")]
                     public abstract record HandleError;
                     
                     public sealed record FailedToDeserialize(string Message) : HandleError;
                     public sealed record InvalidId(int Id) : HandleError;
                     """;

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}