using static OptionSharp.Constructors;
using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Option;

public static class OptionEnumerableExtensions
{
    public static IEnumerable<TResult> FilterMap<T, TResult>(this IEnumerable<Option<T>> source, Func<T, TResult> map) 
        where T : notnull
    {
        foreach (var option in source)
        {
            if (option is Some<T> some) yield return map(some.Value);
        }
    }

    public static async Task<IEnumerable<TResult>> FilterMapAsync<T, TResult>(this Task<IEnumerable<Option<T>>> source, Func<T, TResult> map) 
        where T : notnull =>
        (await source).FilterMap(map);

    public static async IAsyncEnumerable<TResult> FilterMapAsyncEnumerable<T, TResult>(this IAsyncEnumerable<Option<T>> source, Func<T, TResult> map) 
        where T : notnull
    {
        await foreach (var option in source)
        {
            if (option is Some<T> some)
                yield return map(some.Value);
        }
    }

    public static Option<IEnumerable<T>> Collect<T>(this IEnumerable<Option<T>> source) 
        where T : notnull
    {
        List<T> list = new();
        foreach (var option in source)
        {
            switch (option)
            {
                case Some<T> (var value):
                    list.Add(value);
                    break;
                case None<T>:
                    return None<IEnumerable<T>>();
                default:
                    ThrowOption<IEnumerable<T>>(nameof(option));
                    break;
            }
        }

        return Some(list.AsEnumerable());
    }

    public static async Task<Option<IEnumerable<T>>> CollectAsync<T>(this Task<IEnumerable<Option<T>>> source)
        where T : notnull =>
        (await source).Collect();
    
    public static async Task<Option<IEnumerable<T>>> CollectAsync<T>(this IAsyncEnumerable<Option<T>> source) 
        where T : notnull
    {
        List<T> list = new();
        await foreach (var option in source)
        {
            if (option is Some<T> some)
                list.Add(some.Value);
        }

        return new Some<IEnumerable<T>>(list.AsEnumerable());
    }
}