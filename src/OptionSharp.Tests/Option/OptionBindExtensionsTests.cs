using static OptionSharp.Constructors;
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
    public void Given_None_When_CallingBind_Then_ReturnNone()
    {
        None<int> option = new();
        Option<int> result = option.Bind(_ => new None<int>());

        Assert.IsType<None<int>>(result);
    }

    
    [Fact]
    public async Task Given_Some_When_CallingBindAsync_Then_ReturnSome()
    {
        Some<int> option = new(7);
        Option<int> result = await option.BindAsync(i => Task.FromResult(Some(i * i)));

        Assert.IsType<Some<int>>(result);
        var some = result as Some<int>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public async Task Given_None_When_CallingBindAsync_Then_ReturnNone()
    {
        None<int> option = new();
        Option<int> result = await option.BindAsync(_ => Task.FromResult(None<int>()));
    
        Assert.IsType<None<int>>(result);
    }
    
    [Fact]
    public async Task Given_Some_When_CallingBindAsyncOnTask_Then_ReturnSome()
    {
        var task = Task.FromResult(Some(7));
        Option<int> result = await task.BindAsync(i => Some(i * i));

        Assert.IsType<Some<int>>(result);
        var some = result as Some<int>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public async Task Given_None_When_CallingBindAsyncOnTask_Then_ReturnNone()
    {
        var task = Task.FromResult(None<int>());
        Option<int> result = await task.BindAsync(_ => None<int>());

        Assert.IsType<None<int>>(result);
    }
    
    [Fact]
    public async Task Given_Some_When_CallingBindAsyncOnTaskWithTask_Then_ReturnSome()
    {
        var task = Task.FromResult(Some(7));
        Option<int> result = await task.BindAsync(i => Task.FromResult(Some(i * i)));

        Assert.IsType<Some<int>>(result);
        var some = result as Some<int>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public async Task Given_None_When_CallingBindAsyncOnTaskWithTask_Then_ReturnNone()
    {
        var task = Task.FromResult(None<int>());
        Option<int> result = await task.BindAsync(_ => Task.FromResult(None<int>()));

        Assert.IsType<None<int>>(result);
    }
}