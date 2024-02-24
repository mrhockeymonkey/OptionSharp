using static OptionSharp.ThrowHelpers;
using static OptionSharp.Constructors;

namespace OptionSharp;

public static class ResultExtensions
{
    public static bool IsOk<T, TErr>(this Result<T, TErr> result) =>
        result switch
        {
            Ok<T, TErr> => true,
            Err<T, TErr> => false,
            _ => Throw(nameof(result))
        };
    
    public static bool IsOkAnd<T, TErr>(this Result<T, TErr> result, Func<T, bool> predicate) =>
        result switch
        {
            Ok<T, TErr> ok => predicate(ok.Value),
            Err<T, TErr> => false,
            _ => Throw(nameof(result))
        };
    
    public static bool IsErr<T, TErr>(this Result<T, TErr> result) =>
        result switch
        {
            Ok<T, TErr> => false,
            Err<T, TErr> => true,
            _ => Throw(nameof(result))
        };
    
    public static Result<TResult, TErr> Map<T, TErr, TResult>(this Result<T, TErr> result, Func<T, TResult> map) =>
        result switch
        {
            Ok<T, TErr> ok => Ok<TResult, TErr>(map(ok.Value)),
            Err<T, TErr> err => Err<TResult, TErr>(err.Error),
            _ => ThrowResult<TResult, TErr>(nameof(result))
        };
    
    public static async Task<Result<TResult, TErr>> MapAsync<T, TErr, TResult>(this Task<Result<T, TErr>> result, Func<T, TResult> map)
    {
        var awaitedResult = await result;
        return awaitedResult switch
        {
            Ok<T, TErr> ok => Ok<TResult, TErr>(map(ok.Value)),
            Err<T, TErr> err => Err<TResult, TErr>(err.Error),
            _ => ThrowResult<TResult, TErr>(nameof(result))
        };
    }

    
    public static Result<TResult, TErr> Bind<T, TErr, TResult>(this Result<T, TErr> result, Func<T, Result<TResult, TErr>> andThen) =>
        result switch
        {
            Ok<T, TErr> ok => andThen(ok.Value),
            Err<T, TErr> err => Err<TResult, TErr>(err.Error),
            _ => ThrowResult<TResult, TErr>(nameof(result))
        };
    
    public static Task<Result<TResult, TErr>> BindAsync<T, TErr, TResult>(this Result<T, TErr> result, Func<T, Task<Result<TResult, TErr>>> andThen) =>
        result switch
        {
            Ok<T, TErr> ok => andThen(ok.Value),
            Err<T, TErr> err => Task.FromResult(Err<TResult, TErr>(err.Error)),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };

    public static Result<T, TErr> Inspect<T, TErr>(this Result<T, TErr> result, Action<T> inspect)
    {
        if (result is Ok<T, TErr> okay)
        {
            inspect(okay.Value);
        }
        
        return result switch
        {
            Ok<T, TErr> ok => ok,
            Err<T, TErr> err => err,
            _ => ThrowResult<T, TErr>(nameof(result))
        };
    }
    
    public static async Task<Result<T, TErr>> InspectAsync<T, TErr>(this Task<Result<T, TErr>> result, Action<T> inspect)
    {
        var awaitedResult = await result;
        if (awaitedResult is Ok<T, TErr> okay)
        {
            inspect(okay.Value);
        }
        
        return awaitedResult switch
        {
            Ok<T, TErr> ok => ok,
            Err<T, TErr> err => err,
            _ => ThrowResult<T, TErr>(nameof(result))
        };
    }
    
    public static T Reduce<T, TErr>(this Result<T, TErr> result, Func<T> @else) =>
        result switch
        {
            Ok<T, TErr> ok => ok.Value,
            Err<T, TErr> err => @else(),
            _ => Throw<T>(nameof(result))
        };




}