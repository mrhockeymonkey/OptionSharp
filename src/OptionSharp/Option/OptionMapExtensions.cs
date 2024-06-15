using static OptionSharp.Constructors;
using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Option;

public static class OptionMapExtensions
{
    
    public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> map) 
        where T : notnull 
        where TResult : notnull =>
        option switch
        {
            Some<T> some => Some(map(some.Value)),
            None<T> => None<TResult>(),
            _ => ThrowOption<TResult>(nameof(option))
        };
    
    public static async Task<Option<TResult>> MapAsync<T, TResult>(this Option<T> option, Func<T, Task<TResult>> map) 
        where T : notnull 
        where TResult : notnull =>
        option switch
        {
            Some<T> some => Some(await map(some.Value)),
            None<T> => None<TResult>(),
            _ => ThrowOption<TResult>(nameof(option))
        };

    public static async Task<Option<TResult>> MapAsync<T, TResult>(this Task<Option<T>> option, Func<T, TResult> map) 
        where TResult : notnull 
        where T : notnull => 
        (await option).Map(map);

    public static async Task<Option<TResult>> MapAsync<T, TResult>(this Task<Option<T>> option, Func<T, Task<TResult>> map) 
        where TResult : notnull 
        where T : notnull =>
        await (await option).MapAsync(map);
}