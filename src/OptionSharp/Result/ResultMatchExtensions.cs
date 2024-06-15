using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Result;

public static class ResultMatchExtensions
{
    public static Unit Match<T, TErr>(this Result<T, TErr> result, Action<T> ok, Action<TErr> err) 
        where T : notnull
        where TErr : notnull
    {
        switch (result)
        {
            case Ok<T, TErr> o:
                ok(o.Value);
                break;
            case Err<T, TErr> e:
                err(e.Error);
                break;
            default:
                Throw<Unit>(nameof(result));
                break;
        }

        return Unit.Value;
    }

    public static async Task<Unit> MatchAsync<T, TErr>(this Task<Result<T, TErr>> result, Action<T> ok, Action<TErr> err) 
        where TErr : notnull 
        where T : notnull =>
        (await result).Match(ok, err);

    
    public static TResult Match<T, TErr, TResult>(this Result<T, TErr> result, Func<T, TResult> ok, Func<TErr, TResult> err) 
        where T : notnull
        where TErr : notnull =>
        result switch
        {
            Ok<T, TErr> o => ok(o.Value),
            Err<T, TErr> e => err(e.Error),
            _ => Throw<TResult>(nameof(result))
        };

    public static async Task<TResult> MatchAsync<T, TErr, TResult>(this Task<Result<T, TErr>> result, Func<T, TResult> ok, Func<TErr, TResult> err)
        where T : notnull
        where TErr : notnull =>
        (await result).Match(ok, err);

}