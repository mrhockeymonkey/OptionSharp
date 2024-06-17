using OptionSharp.Result;
using static OptionSharp.Constructors;

namespace OptionSharp.Tests.Result;


public class ResultBindExtensionsTests
{
    [Fact]
    public void Given_Ok_When_CallingBind_Then_ReturnOk()
    {
        var ok = Ok(7);
        var result = ok.Bind(i => Ok(i * i));
        
        ResultAssert.IsOk(result, i => Assert.Equal(49, i));
    }
    
    [Fact]
    public void Given_Err_When_CallingBind_Then_ReturnErr()
    {
        var err = Err<int>("NaN");
        var result = err.Bind(i => Ok(i * i));
    
        ResultAssert.IsErr(result, m => Assert.Equal("NaN", m.Message));
    }
    
    [Fact]
    public async Task Given_Ok_When_CallingBindAsync_Then_ReturnOk()
    {
        var ok = Ok(7);
        var result = await ok.BindAsync(i => Task.FromResult(Ok(i * i)));
        
        ResultAssert.IsOk(result, i => Assert.Equal(49, i));
    }
    
    [Fact]
    public async Task Given_Err_When_CallingBindAsync_Then_ReturnErr()
    {
        var err = Err<int>("NaN");
        var result = await err.BindAsync(i => Task.FromResult(Ok(i * i)));
    
        ResultAssert.IsErr(result, m => Assert.Equal("NaN", m.Message));
    }
    
    [Fact]
    public async Task Given_Ok_When_CallingBindAsyncOnTask_Then_ReturnOk()
    {
        var task = Task.FromResult(Ok(7));
        var result = await task.BindAsync(i => Ok(i * i));
    
        ResultAssert.IsOk(result, i => Assert.Equal(49, i));
    }
    
    [Fact]
    public async Task Given_Err_When_CallingBindAsyncOnTask_Then_ReturnErr()
    {
        var task = Task.FromResult(Err<int>("NaN"));
        var result = await task.BindAsync(i => Ok(i * i));
    
        ResultAssert.IsErr(result, m => Assert.Equal("NaN", m.Message));
    }
    
    [Fact]
    public async Task Given_Ok_When_CallingBindAsyncOnTaskWithTask_Then_ReturnOk()
    {
        var task = Task.FromResult(Ok(7));
        var result = await task.BindAsync(i => Task.FromResult(Ok(i * i)));
    
        ResultAssert.IsOk(result, i => Assert.Equal(49, i));
    }
    
    [Fact]
    public async Task Given_Err_When_CallingBindAsyncOnTaskWithTask_Then_ReturnErr()
    {
        var task = Task.FromResult(Err<int>("NaN"));
        var result = await task.BindAsync(i => Task.FromResult(Ok(i * i)));
    
        ResultAssert.IsErr(result, m => Assert.Equal("NaN", m.Message));
    }
}