using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Option;

public static class OptionMatchExtensions
{
    public static Unit Match<T>(this Option<T> option, Action<T> some, Action none) 
        where T : notnull
    {
        if (option is Some<T> s)
            some(s.Value);
        else
            none();

        return Unit.Value;
    }
    
    public static async Task<Unit> MatchAsync<T>(this Task<Option<T>> option, Action<T> some, Action none) 
        where T : notnull =>
        (await option).Match(some, none);
    

    public static TResult Match<T, TResult>(this Option<T> option, Func<T, TResult> some, Func<TResult> none) 
        where T : notnull =>
        option switch
        {
            Some<T> s => some(s.Value),
            None<T> => none(),
            _ => Throw<TResult>(nameof(option))
        };

    
    public static async Task<TResult> MatchAsync<T, TResult>(this Task<Option<T>> option, Func<T, TResult> some, Func<TResult> none)
        where T : notnull =>
        (await option).Match(some, none);
}