using static OptionSharp.Constructors;
using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Result;

public static class ResultEnumerableExtensions
{
    public static IEnumerable<TResult> FilterMap<T, TErr, TResult>(this IEnumerable<Result<T, TErr>> source, Func<T, TResult> map) 
        where T : notnull
    {
        foreach (var result in source)
        {
            if (result is Ok<T, TErr> some) yield return map(some.Value);
        }
    }
    
    public static async Task<IEnumerable<TResult>> FilterMapAsync<T, TErr, TResult>(this Task<IEnumerable<Result<T, TErr>>> source, Func<T, TResult> map) 
        where T : notnull =>
        (await source).FilterMap(map);
    
    public static async IAsyncEnumerable<TResult> FilterMapAsyncEnumerable<T, TErr, TResult>(this IAsyncEnumerable<Result<T, TErr>> source, Func<T, TResult> map) 
        where T : notnull
    {
        await foreach (var option in source)
        {
            if (option is Ok<T, TErr> some)
                yield return map(some.Value);
        }
    }
    
    public static Result<IEnumerable<T>, TErr> Collect<T, TErr>(this IEnumerable<Result<T, TErr>> source) 
        where T : notnull 
        where TErr : notnull
    {
        List<T> list = new();
        foreach (var result in source)
        {
            switch (result)
            {
                case Ok<T, TErr> (var value):
                    list.Add(value);
                    break;
                case Err<T, TErr> (var error):
                    return Err<IEnumerable<T>, TErr>(error);
                default:
                    ThrowResult<IEnumerable<T>, TErr>(nameof(result));
                    break;
            }
        }
    
        return Ok<IEnumerable<T>, TErr>(list.AsEnumerable());
    }
    
    public static async Task<Result<IEnumerable<T>, TErr>> CollectAsync<T, TErr>(this Task<IEnumerable<Result<T, TErr>>> source)
        where T : notnull 
        where TErr : notnull =>
        (await source).Collect();
    
    public static async Task<Result<IEnumerable<T>, TErr>> CollectAsync<T, TErr>(this IAsyncEnumerable<Result<T, TErr>> source) 
        where T : notnull
    {
        List<T> list = new();
        await foreach (var result in source)
        {
            if (result is Ok<T, TErr> ok)
                list.Add(ok.Value);
        }
    
        return new Ok<IEnumerable<T>, TErr>(list.AsEnumerable());
    }
}