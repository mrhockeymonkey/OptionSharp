namespace OptionSharp;

public static class EnumerableExtensions
{
    public static IEnumerable<TResult> FilterMap<T, TResult>(this IEnumerable<Option<T>> source, Func<T, TResult> map) 
        where T : notnull
    {
        foreach (var option in source)
        {
            if (option is Some<T> some)
                yield return map(some.Value);
        }
    }
    
    public static async IAsyncEnumerable<TResult> FilterMap<T, TResult>(this IAsyncEnumerable<Option<T>> source, Func<T, TResult> map) 
        where T : notnull
    {
        await foreach (var option in source)
        {
            if (option is Some<T> some)
                yield return map(some.Value);
        }
    }
}