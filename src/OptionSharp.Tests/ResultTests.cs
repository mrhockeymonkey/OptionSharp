namespace OptionSharp.Tests;

public class ResultTests
{
    [Fact]
    public void Given_ValueType_When_ConstructingOk_Then_ReturnOk()
    {
        Ok<int, int> ok = new(7);
        Assert.Equal(7, ok.Value);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingOk_Then_ReturnOk()
    {
        Ok<string, int> ok = new("seven");
        Assert.Equal("seven", ok.Value);
    }
    
    [Fact]
    public void Given_NullRefType_When_ConstructingOk_Then_ReturnOk()
    {
        Assert.Throws<ArgumentNullException>(() => new Ok<string, int>(default!));
    }
    
    [Fact]
    public void Given_ValueType_When_ConstructingErr_Then_ReturnErr()
    {
        Err<int, int> err = new(0);
        Assert.Equal(0, err.Error);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingErr_Then_ReturnErr()
    {
        Err<string, string> err = new("error");
        Assert.Equal("error", err.Error);
    }
    
    [Fact]
    public void Given_NullRefType_When_ConstructingErr_Then_ReturnErr()
    {
        Assert.Throws<ArgumentNullException>(() => new Err<string, string>(default!));
    }
    

}