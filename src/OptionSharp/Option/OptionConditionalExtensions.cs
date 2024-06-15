using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Option;

public static class OptionConditionalExtensions
{
    public static bool IsSome<T>(this Option<T> option) 
        where T : notnull =>
        option switch
        {
            Some<T> => true,
            None<T> => false,
            _ => Throw(nameof(option))
        };
    
    public static bool IsSomeAnd<T>(this Option<T> option, Func<T, bool> predicate) 
        where T : notnull =>
        option switch
        {
            Some<T> some => predicate(some.Value),
            None<T> => false,
            _ => Throw(nameof(option))
        };
    
    public static bool IsNone<T>(this Option<T> option) 
        where T : notnull =>
        option switch
        {
            Some<T> => false,
            None<T> => true,
            _ => Throw(nameof(option))
        };
}