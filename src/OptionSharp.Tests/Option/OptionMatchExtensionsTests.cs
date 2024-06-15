using OptionSharp.Option;

namespace OptionSharp.Tests.Option;

public class OptionMatchExtensionsTests
{
    
    [Fact]
    public void GivenSome_WhenCallingMatch_ThenRunAction()
    {
        int i = 0;
        Some<int> some = new(7);

        var matched = some.Match(
            n => { i = n; }, 
            () => { i = -1; });
        
        Assert.Equal(7, i);
        Assert.Equal(Unit.Value, matched);
    }
    
    [Fact]
    public void GivenNone_WhenCallingMatch_ThenRunAction()
    {
        int i = 0;
        None<int> none = new();

        var matched = none.Match(
            n => { i = n; }, 
            () => { i = -1; });
        
        Assert.Equal(-1, i);
        Assert.Equal(Unit.Value,  matched);
    }
    
    [Fact]
    public void GivenSome_WhenCallingMatch_ThenRunFunc()
    {
        Some<int> some = new(7);

        var matched = some.Match(
            n => n, 
            () => -1);
        
        Assert.Equal(7, matched);
    }
    
    [Fact]
    public void GivenNone_WhenCallingMatch_ThenRunFunc()
    {
        None<int> none = new();

        var matched = none.Match(
            n => n, 
            () => -1);
        
        Assert.Equal(-1, matched);
    }
}