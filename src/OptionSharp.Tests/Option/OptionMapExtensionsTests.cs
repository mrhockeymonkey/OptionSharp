using OptionSharp.Option;

namespace OptionSharp.Tests.Option;

public class OptionMapExtensionsTests
{
    [Fact]
    public void Given_Some_When_CallingMap_Then_ReturnSomeMapped()
    {
        Some<int> option = new(7);
        Option<int> result = option.Map(i => i * i);

        Assert.IsType<Some<int>>(result);
        var some = result as Some<int>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public void Given_None_When_CallingMap_Then_ReturnSomeMapped()
    {
        None<int> option = new();
        Option<int> result = option.Map(i => i * i);

        Assert.IsType<None<int>>(result);
    }
}