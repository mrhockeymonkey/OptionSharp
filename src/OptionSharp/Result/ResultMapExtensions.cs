using static OptionSharp.Constructors;
using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Result;

public static class ResultMapExtensions
{
    
    // map values
    public static Result<TResult, TErr> Map<T, TErr, TResult>(this Result<T, TErr> result, Func<T, TResult> map) 
        where TResult : notnull 
        where TErr : notnull =>
        result switch
        {
            Ok<T, TErr> ok => Ok<TResult, TErr>(map(ok.Value)),
            Err<T, TErr> err => Err<TResult, TErr>(err.Error),
            _ => ThrowResult<TResult, TErr>(nameof(result))
        };
    
    public static async Task<Result<TResult, TErr>> MapAsync<T, TErr, TResult>(this Result<T, TErr> result, Func<T, Task<TResult>> map) 
        where TResult : notnull 
        where TErr : notnull =>
        result switch
        {
            Ok<T, TErr> ok => Ok<TResult, TErr>(await map(ok.Value)),
            Err<T, TErr> err => Err<TResult, TErr>(err.Error),
            _ => ThrowResult<TResult, TErr>(nameof(result))
        };

    public static async Task<Result<TResult, TErr>> MapAsync<T, TErr, TResult>(this Task<Result<T, TErr>> result, Func<T, Task<TResult>> map) 
        where TResult : notnull 
        where TErr : notnull =>
        await (await result).MapAsync(map);


    public static async Task<Result<TResult, TErr>> MapAsync<T, TErr, TResult>(this Task<Result<T, TErr>> result, Func<T, TResult> map) 
        where TResult : notnull 
        where TErr : notnull =>
        (await result).Map(map);
    
    // map errors
    public static Result<T, TResult> MapErr<T, TErr, TResult>(this Result<T, TErr> result, Func<TErr, TResult> map) 
        where TResult : notnull 
        where TErr : notnull =>
        result switch
        {
            Ok<T, TErr> ok => Ok<T, TResult>(ok.Value),
            Err<T, TErr> err => Err<T, TResult>(map(err.Error)),
            _ => ThrowResult<T, TResult>(nameof(result))
        };
    
    public static async Task<Result<T, TResult>> MapErrAsync<T, TErr, TResult>(this Result<T, TErr> result, Func<TErr, Task<TResult>> map) 
        where TResult : notnull 
        where TErr : notnull =>
        result switch
        {
            Ok<T, TErr> ok => Ok<T, TResult>(ok.Value),
            Err<T, TErr> err => Err<T, TResult>(await map(err.Error)),
            _ => ThrowResult<T, TResult>(nameof(result))
        };

    public static async Task<Result<T, TResult>> MapErrAsync<T, TErr, TResult>(this Task<Result<T, TErr>> result, Func<TErr, Task<TResult>> map) 
        where TResult : notnull 
        where TErr : notnull =>
        await (await result).MapErrAsync(map);


    public static async Task<Result<T, TResult>> MapErrAsync<T, TErr, TResult>(this Task<Result<T, TErr>> result, Func<TErr, TResult> map) 
        where TResult : notnull 
        where TErr : notnull =>
        (await result).MapErr(map);
}