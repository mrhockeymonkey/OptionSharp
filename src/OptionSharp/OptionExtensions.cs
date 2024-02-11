using System.Diagnostics.CodeAnalysis;
using static OptionSharp.Constructors;
using static OptionSharp.ThrowHelpers;

namespace OptionSharp;

public static class OptionExtensions
{
    public static bool IsSome<T>(this Option<T> option) =>
        option switch
        {
            Some<T> some => true,
            None<T> none => false,
            _ => Throw(nameof(option))
        };
    
    public static bool IsSomeAnd<T>(this Option<T> option, Func<T, bool> predicate) =>
        option switch
        {
            Some<T> some => predicate(some.Value),
            None<T> none => false,
            _ => Throw(nameof(option))
        };
    
    public static bool IsNone<T>(this Option<T> option) =>
        option switch
        {
            Some<T> some => false,
            None<T> none => true,
            _ => Throw(nameof(option))
        };
    
    public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> map) =>
        option switch
        {
            Some<T> some => Some(map(some.Value)),
            None<T> none => None<TResult>(),
            _ => ThrowOption<TResult>(nameof(option))
        };
    
    /// <summary>
    /// Allows you to chain multiple operations that return options together.
    /// </summary>
    /// <param name="option"></param>
    /// <param name="andThen"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static Option<TResult> Bind<T, TResult>(this Option<T> option, Func<T, Option<TResult>> andThen) =>
        option switch
        {
            Some<T> some => andThen(some.Value),
            None<T> none => None<TResult>(),
            _ => ThrowOption<TResult>(nameof(option))
        };

    public static Option<T> Inspect<T>(this Option<T> option, Action<T> inspect)
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
    
    public static T Reduce<T>(this Option<T> option, Func<T> @else) =>
        option switch
        {
            Some<T> some => some.Value,
            None<T> none => @else(),
            _ => Throw<T>(nameof(option))
        };
}