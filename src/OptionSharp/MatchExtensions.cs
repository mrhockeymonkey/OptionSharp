using static OptionSharp.ThrowHelpers;

namespace OptionSharp;

public static class MatchExtensions
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
    
    public static TResult Match<T, TResult>(this Option<T> option, Func<T, TResult> some, Func<TResult> none) 
        where T : notnull =>
        option switch
        {
            Some<T> s => some(s.Value),
            None<T> => none(),
            _ => Throw<TResult>(nameof(option))
        };
}