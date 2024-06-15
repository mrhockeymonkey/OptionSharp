using OptionSharp.Result;

namespace OptionSharp.Tests.Result;

public class ResultMatchExtensionsTests
{
    
    [Fact]
    public void GivenOk_WhenCallingMatch_ThenRunAction()
    {
        int i = 0;
        Ok<int, ErrMessage> ok = new(7);

        var matched = ok.Match(
            n => { i = n; }, 
            e => { i = -1; });
        
        Assert.Equal(7, i);
        Assert.Equal(Unit.Value, matched);
    }
    
    [Fact]
    public void GivenErr_WhenCallingMatch_ThenRunAction()
    {
        int i = 0;
        Err<int, ErrMessage> err = new(new("error"));

        var matched = err.Match(
            n => { i = n; }, 
            e => { i = -1; });
        
        Assert.Equal(-1, i);
        Assert.Equal(Unit.Value,  matched);
    }
    
    [Fact]
    public void GivenOk_WhenCallingMatch_ThenRunFunc()
    {
        Ok<int, ErrMessage> ok = new(7);

        var matched = ok.Match(
            n => n, 
            e => -1);
        
        Assert.Equal(7, matched);
    }
    
    [Fact]
    public void GivenErr_WhenCallingMatch_ThenRunFunc()
    {
        Err<int, ErrMessage> err = new(new("error"));

        var matched = err.Match(
            n => n, 
            e => -1);
        
        Assert.Equal(-1, matched);
    }
}