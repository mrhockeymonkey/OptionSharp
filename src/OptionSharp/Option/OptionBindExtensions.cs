using static OptionSharp.ThrowHelpers;
using static OptionSharp.Constructors;

namespace OptionSharp.Option;

public static class OptionBindExtensions
{
    public static Option<TResult> Bind<T, TResult>(this Option<T> option, Func<T, Option<TResult>> andThen) 
        where T : notnull
        where TResult : notnull =>
        option switch
        {
            Some<T> some => andThen(some.Value),
            None<T> => None<TResult>(),
            _ => ThrowOption<TResult>(nameof(option))
        };

    public static Task<Option<TResult>> BindAsync<T, TResult>(this Option<T> option, Func<T, Task<Option<TResult>>> andThen)
        where T : notnull
        where TResult : notnull =>
        option switch
        {
            Some<T> some => andThen(some.Value),
            None<T> => Task.FromResult(None<TResult>()),
            _ => throw new ArgumentOutOfRangeException(nameof(option))
        };

    public static async Task<Option<TResult>> BindAsync<T, TResult>(this Task<Option<T>> option, Func<T, Option<TResult>> andThen)
        where T : notnull
        where TResult : notnull =>
        (await option).Bind(andThen);

    public static async Task<Option<TResult>> BindAsync<T, TResult>(this Task<Option<T>> option, Func<T, Task<Option<TResult>>> andThen)
        where T : notnull
        where TResult : notnull =>
        await (await option).BindAsync(andThen);

}