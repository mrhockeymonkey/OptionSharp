using static OptionSharp.Constructors;
using static OptionSharp.ThrowHelpers;

namespace OptionSharp;

public static class OptionExtensions
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
    
    public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> map) 
        where T : notnull 
        where TResult : notnull =>
        option switch
        {
            Some<T> some => Some(map(some.Value)),
            None<T> => None<TResult>(),
            _ => ThrowOption<TResult>(nameof(option))
        };
    
    
    /// Allows you to chain multiple operations that return options together.
    public static Option<TResult> Bind<T, TResult>(this Option<T> option, Func<T, Option<TResult>> andThen) 
        where T : notnull
        where TResult : notnull =>
        option switch
        {
            Some<T> some => andThen(some.Value),
            None<T> => None<TResult>(),
            _ => ThrowOption<TResult>(nameof(option))
        };

    public static Option<T> Inspect<T>(this Option<T> option, Action<T> inspect)
        where T : notnull
    {
        if (option is Some<T> s)
        {
            inspect(s.Value);
        }
        
        return option switch
        {
            Some<T> some => some,
            None<T> none => none,
            _ => ThrowOption<T>(nameof(option))
        };
    }
    
    public static T Reduce<T>(this Option<T> option, Func<T> @else) 
        where T : notnull =>
        option switch
        {
            Some<T> some => some.Value,
            None<T> => @else(),
            _ => Throw<T>(nameof(option))
        };
}