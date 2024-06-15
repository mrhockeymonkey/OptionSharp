using OptionSharp.Option;

namespace OptionSharp.Tests.Option;

public class OptionUnwrapExtensionsTests
{
    [Fact]
    public void Given_Some_When_CallingReduce_Then_ReturnSome()
    {
        Some<int> option = new(7);
        int result = option.UnwrapOrDefault(() => 0);
        
        Assert.Equal(7, result);
    }
    
    [Fact]
    public void Given_None_When_CallingReduce_Then_ReturnSome()
    {
        None<int> option = new();
        int result = option.UnwrapOrDefault(() => 0);

        Assert.Equal(0, result);
    }
}