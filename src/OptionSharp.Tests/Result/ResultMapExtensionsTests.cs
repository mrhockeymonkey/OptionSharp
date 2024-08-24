using OptionSharp.Option;
using OptionSharp.Result;

namespace OptionSharp.Tests.Result;

public class ResultMapExtensionsTests
{
    #region Map
    
    [Fact]
    public void GivenOk_WhenCallingMap_ThenReturnOkMapped()
    {
        Ok<int, ErrMessage> ok = new(7);
        Result<int, ErrMessage> result = ok.Map(i => i * i);

        Assert.IsType<Ok<int, ErrMessage>>(result);
        var some = result as Ok<int, ErrMessage>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public void GivenErr_WhenCallingMap_ThenReturnErr()
    {
        Err<int, ErrMessage> error = new(new ErrMessage("message"));
        Result<int, ErrMessage> result = error.Map(i => i * i);

        Assert.IsType<Err<int, ErrMessage>>(result);
    }

    #endregion

    #region MapAsync

    [Fact]
    public async Task GivenOk_WhenCallingMapWithAsyncFunc_ThenReturnOkMapped()
    {
        Ok<int, ErrMessage> ok = new(7);
        Result<int, ErrMessage> result = await ok.MapAsync(i => Task.FromResult(i * i));

        Assert.IsType<Ok<int, ErrMessage>>(result);
        var some = result as Ok<int, ErrMessage>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public async Task GivenErr_WhenCallingMapWithAsyncFunc_ThenReturnErr()
    {
        Err<int, ErrMessage> error = new(new ErrMessage("message"));
        Result<int, ErrMessage> result = await error.MapAsync(i => Task.FromResult(i * i));

        Assert.IsType<Err<int, ErrMessage>>(result);
    }
    
    [Fact]
    public async Task GivenOk_WhenCallingMapWithAsyncFuncFromATask_ThenReturnOkMapped()
    {
        Result<int, ErrMessage> ok = new Ok<int, ErrMessage>(7);
        Task<Result<int, ErrMessage>> okTask = Task.FromResult(ok); 
        Result<int, ErrMessage> result = await okTask.MapAsync(i => Task.FromResult(i * i));

        Assert.IsType<Ok<int, ErrMessage>>(result);
        var some = result as Ok<int, ErrMessage>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public async Task GivenErr_WhenCallingMapWithAsyncFuncFromATask_ThenReturnErr()
    {
        Result<int, ErrMessage> error = new Err<int, ErrMessage>(new ErrMessage("message"));
        Task<Result<int, ErrMessage>> errorTask = Task.FromResult(error);
        Result<int, ErrMessage> result = await errorTask.MapAsync(i => Task.FromResult(i * i));

        Assert.IsType<Err<int, ErrMessage>>(result);
    }
    
    [Fact]
    public async Task GivenOk_WhenCallingMapWithSyncFuncFromATask_ThenReturnOkMapped()
    {
        Result<int, ErrMessage> ok = new Ok<int, ErrMessage>(7);
        Task<Result<int, ErrMessage>> okTask = Task.FromResult(ok); 
        Result<int, ErrMessage> result = await okTask.MapAsync(i => i * i);

        Assert.IsType<Ok<int, ErrMessage>>(result);
        var some = result as Ok<int, ErrMessage>;
        Assert.NotNull(some);
        Assert.Equal(49, some.Value);
    }
    
    [Fact]
    public async Task GivenErr_WhenCallingMapWithSyncFuncFromATask_ThenReturnErr()
    {
        Result<int, ErrMessage> error = new Err<int, ErrMessage>(new ErrMessage("message"));
        Task<Result<int, ErrMessage>> errorTask = Task.FromResult(error);
        Result<int, ErrMessage> result = await errorTask.MapAsync(i => i * i);

        Assert.IsType<Err<int, ErrMessage>>(result);
    }

    #endregion

    #region MapErr
    
    [Fact]
    public void GivenOk_WhenCallingMapErr_ThenReturnOkMapped()
    {
        Ok<int, ErrMessage> ok = new(7);
        Result<int, string> result = ok.MapErr(_ => "something else");

        Assert.IsType<Ok<int, string>>(result);
        var o = result as Ok<int, string>;
        Assert.NotNull(o);
        Assert.Equal(7, o.Value);
    }
    
    [Fact]
    public void GivenErr_WhenCallingMapErr_ThenReturnErr()
    {
        Err<int, ErrMessage> error = new(new ErrMessage("message"));
        Result<int, string> result = error.MapErr(_ => "something else");

        Assert.IsType<Err<int, string>>(result);
        var e = result as Err<int, string>;
        Assert.NotNull(e);
        Assert.Equal("something else", e.Error);
    }

    #endregion
    
    # region MapErrAsync
    
    [Fact]
    public async Task GivenOk_WhenCallingMapErrWithAsyncFunc_ThenReturnOkMapped()
    {
        Result<int, ErrMessage> ok = new Ok<int, ErrMessage>(7);
        Task<Result<int, ErrMessage>> okTask = Task.FromResult(ok);
        Result<int, string> result = await okTask.MapErrAsync(_ => "something else");

        Assert.IsType<Ok<int, string>>(result);
        var o = result as Ok<int, string>;
        Assert.NotNull(o);
        Assert.Equal(7, o.Value);
    }
    
    [Fact]
    public async Task GivenErr_WhenCallingMapErrWithAsyncFunc_ThenReturnErr()
    {
        Result<int, ErrMessage> error = new Err<int, ErrMessage>(new ErrMessage("message"));
        Task<Result<int, ErrMessage>> errorTask = Task.FromResult(error);
        Result<int, string> result = await errorTask.MapErrAsync(_ => "something else");

        Assert.IsType<Err<int, string>>(result);
        var e = result as Err<int, string>;
        Assert.NotNull(e);
        Assert.Equal("something else", e.Error);
    }
    
    # endregion
}