namespace OptionSharp.Tests;

public static class ResultAssert
{
    public static void IsOk<T, TErr>(Result<T, TErr> result, Action<T> assert)
    {
        switch (result)
        {
            case Ok<T, TErr> ok:
                assert(ok.Value);
                break;
            case Err<T, TErr>:
                Assert.Fail($"Expected {nameof(Ok<T, TErr>)} but received {nameof(Err<T, TErr>)}");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public static void IsErr<T, TErr>(Result<T, TErr> result, Action<TErr> assert)
    {
        switch (result)
        {
            case Ok<T, TErr>:
                Assert.Fail($"Expected {nameof(Err<T, TErr>)} but received {nameof(Ok<T, TErr>)}");
                break;
            case Err<T, TErr> err:
                assert(err.Error);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}