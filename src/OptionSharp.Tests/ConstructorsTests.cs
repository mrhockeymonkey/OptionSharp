using static OptionSharp.Constructors;

namespace OptionSharp.Tests;

public class ConstructorsTests
{
    # region Option Constructors
    [Fact]
    public void Given_ValueType_When_ConstructingSome_Then_ReturnSome()
    {
        Option<int> option = Some(7);
        Assert.IsType<Some<int>>(option);
        var some = option as Some<int>;
        Assert.NotNull(some);
        Assert.Equal(7, some.Value);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingSome_Then_ReturnSome()
    {
        Option<string> option = Some("seven");
        Assert.IsType<Some<string>>(option);
        var some = option as Some<string>;
        Assert.NotNull(some);
        Assert.Equal("seven", some.Value);
    }
    
    [Fact]
    public void Given_NullRefType_When_ConstructingSome_Then_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => Some(default(string)!));
    }
    
    [Fact]
    public void Given_ValueType_When_ConstructingNone_Then_ReturnNone()
    {
        Option<int> option = None<int>();
        Assert.IsType<None<int>>(option);
        Assert.NotNull(option);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingNone_Then_ReturnNone()
    {
        Option<string> option = None<string>();
        Assert.IsType<None<string>>(option);
        Assert.NotNull(option);
    }
    
    # endregion

    #region Result Constructors - Generic

    [Fact]
    public void Given_ValueType_When_ConstructingOk_Then_ReturnResult()
    {
        Result<int, int> result = Ok<int, int>(7);
        Assert.IsType<Ok<int, int>>(result);
        var ok = result as Ok<int, int>;
        Assert.NotNull(ok);
        Assert.Equal(7, ok.Value);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingOk_Then_ReturnResult()
    {
        Result<string, int> result = Ok<string, int>("seven");
        Assert.IsType<Ok<string, int>>(result);
        var ok = result as Ok<string, int>;
        Assert.NotNull(ok);
        Assert.Equal("seven", ok.Value);
    }
    
    [Fact]
    public void Given_NullRefType_When_ConstructingOk_Then_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => Ok<string, int>(default(string)!));
    }
    
    [Fact]
    public void Given_ValueType_When_ConstructingErr_Then_ReturnResult()
    {
        Result<int, int> result = Err<int, int>(0);
        Assert.IsType<Err<int, int>>(result);
        var err = result as Err<int, int>;
        Assert.NotNull(err);
        Assert.Equal(0, err.Error);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingErr_Then_ReturnResult()
    {
        Result<int, string> result = Err<int, string>("error");
        Assert.IsType<Err<int, string>>(result);
        var err = result as Err<int, string>;
        Assert.NotNull(err);
        Assert.Equal("error", err.Error);
    }
    
    [Fact]
    public void Given_NullRefType_When_ConstructingErr_Then_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => Err<int, string>(default(string)!));
    }

    #endregion
    
    #region Result Constructors - ErrMessage

    [Fact]
    public void Given_ValueType_When_ConstructingOkWithErrMessage_Then_ReturnResult()
    {
        Result<int, ErrMessage> result = Ok<int>(7);
        Assert.IsType<Ok<int, ErrMessage>>(result);
        var ok = result as Ok<int, ErrMessage>;
        Assert.NotNull(ok);
        Assert.Equal(7, ok.Value);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingOkWithErrMessage_Then_ReturnResult()
    {
        Result<string, ErrMessage> result = Ok<string>("seven");
        Assert.IsType<Ok<string, ErrMessage>>(result);
        var ok = result as Ok<string, ErrMessage>;
        Assert.NotNull(ok);
        Assert.Equal("seven", ok.Value);
    }
    
    [Fact]
    public void Given_NullRefType_When_ConstructingOkWithErrMessage_Then_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => Ok<string>(default(string)!));
    }
    
    [Fact]
    public void Given_ValueType_When_ConstructingErrWithErrMessage_Then_ReturnResult()
    {
        Result<int, ErrMessage> result = Err<int>("error");
        Assert.IsType<Err<int, ErrMessage>>(result);
        var err = result as Err<int, ErrMessage>;
        Assert.NotNull(err);
        Assert.Equal(new ErrMessage("error"), err.Error);
    }
    
    [Fact]
    public void Given_RefType_When_ConstructingErrWithErrMessage_Then_ReturnResult()
    {
        Result<int, ErrMessage> result = Err<int>("error");
        Assert.IsType<Err<int, ErrMessage>>(result);
        var err = result as Err<int, ErrMessage>;
        Assert.NotNull(err);
        Assert.Equal(new ErrMessage("error"), err.Error);
    }
    
    [Fact]
    public void Given_NullRefType_When_ConstructingErrWithErrMessage_Then_Throw()
    {
        Assert.Throws<ArgumentNullException>(() => Err<int>(default(string)!));
    }

    #endregion
}