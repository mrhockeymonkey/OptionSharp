using OptionSharp.Option;

namespace OptionSharp.Tests.Option;

public class OptionBindExtensionsTests
{
    
    [Fact]
    public void Given_Some_When_CallingBind_Then_ReturnSome()
    {
        Some<int> option = new(7);
        Option<int> result = option.Bind(i => new Some<int>(i * i));

        Assert.IsType<Some<int>>(result);
        var some = result as Some<int>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public void Given_None_When_CallingBind_Then_ReturnSome()
    {
        None<int> option = new();
        Option<int> result = option.Bind(_ => new None<int>());

        Assert.IsType<None<int>>(result);
    }
}