namespace OptionSharp.Tests;

public class OptionsExtensionsTests
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
    
    [Fact]
    public void Given_Some_When_CallingReduce_Then_ReturnSome()
    {
        Some<int> option = new(7);
        int result = option.Reduce(() => 0);
        
        Assert.Equal(7, result);
    }
    
    [Fact]
    public void Given_None_When_CallingReduce_Then_ReturnSome()
    {
        None<int> option = new();
        int result = option.Reduce(() => 0);

        Assert.Equal(0, result);
    }
}