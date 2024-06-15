using OptionSharp.Option;

namespace OptionSharp.Tests.Option;

public class OptionInspectExtensionsTests
{
    [Fact]
    public void Given_Some_When_CallingInspect_Then_ReturnSome()
    {
        int flag = 0;
        Some<int> option = new(7);
        option.Inspect(i => flag = i);
        
        Assert.Equal(7, flag);
    }
    
    [Fact]
    public void Given_None_When_CallingInspect_Then_ReturnSome()
    {
        int flag = 0;
        None<int> option = new();
        option.Inspect(i => flag = i);

        Assert.Equal(0, flag);
    }
}