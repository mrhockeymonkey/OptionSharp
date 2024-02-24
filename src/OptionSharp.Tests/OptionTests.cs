namespace OptionSharp.Tests;

public class OptionTests
{
    [Fact]
    public void Given_ValueType_When_ConstructingSome_Then_ReturnSome()
    {
        Some<int> some = new(7);
        Assert.Equal(7, some.Value);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingSome_Then_ReturnSome()
    {
        Some<string> some = new("seven");
        Assert.Equal("seven", some.Value);
    }
    
    [Fact]
    public void Given_NullRefType_When_ConstructingSome_Then_ReturnSome()
    {
        Assert.Throws<ArgumentNullException>(() => new Some<string>(default!));
    }
}