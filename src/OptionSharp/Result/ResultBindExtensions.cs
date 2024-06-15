using static OptionSharp.ThrowHelpers;
using static OptionSharp.Constructors;

namespace OptionSharp.Result;

public static class ResultBindExtensions
{
    public static Result<TResult, TErr> Bind<T, TErr, TResult>(this Result<T, TErr> result, Func<T, Result<TResult, TErr>> andThen) 
        where TErr : notnull =>
        result switch
        {
            Ok<T, TErr> ok => andThen(ok.Value),
            Err<T, TErr> err => Err<TResult, TErr>(err.Error),
            _ => ThrowResult<TResult, TErr>(nameof(result))
        };
    
    public static Task<Result<TResult, TErr>> BindAsync<T, TErr, TResult>(this Result<T, TErr> result, Func<T, Task<Result<TResult, TErr>>> andThen) 
        where TErr : notnull =>
        result switch
        {
            Ok<T, TErr> ok => andThen(ok.Value),
            Err<T, TErr> err => Task.FromResult(Err<TResult, TErr>(err.Error)),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
    
    public static async Task<Result<TResult, TErr>> BindAsync<T, TErr, TResult>(this Task<Result<T, TErr>> result, Func<T, Result<TResult, TErr>> andThen) 
        where TErr : notnull =>
        (await result).Bind(andThen);

    public static async Task<Result<TResult, TErr>> BindAsync<T, TErr, TResult>(this Task<Result<T, TErr>> result, Func<T, Task<Result<TResult, TErr>>> andThen) 
        where TErr : notnull
        where TResult : notnull =>
        await (await result).BindAsync(andThen);
}