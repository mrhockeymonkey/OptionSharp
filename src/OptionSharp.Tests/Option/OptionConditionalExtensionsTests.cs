using OptionSharp.Option;

namespace OptionSharp.Tests.Option;

public class OptionConditionalExtensionsTests
{
    [Fact]
    public void Given_Some_When_CallingConditionalMethods_Then_ReturnCorrectly()
    {
        Some<int> some = new(7);
        Assert.True(some.IsSome());
        Assert.True(some.IsSomeAnd(i => i == 7));
        Assert.False(some.IsNone());
    }
    
    [Fact]
    public void Given_None_When_CallingConditionalMethods_Then_ReturnCorrectly()
    {
        None<int> some = new();
        Assert.False(some.IsSome());
        Assert.False(some.IsSomeAnd(i => i == 7));
        Assert.True(some.IsNone());
    }
}