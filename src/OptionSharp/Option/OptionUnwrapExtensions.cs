using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Option;

public static class OptionUnwrapExtensions
{
    /// <summary>
    /// Returns the underlying value from Some<T> or throws!
    /// Usage is discouraged, prefer to use Reduce, Match or other means to handle Some and None cases.
    /// </summary>
    /// <param name="option"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static T Unwrap<T>(this Option<T> option)
        where T : notnull =>
        option switch
        {
            Some<T> some => some.Value,
            None<T> => throw new ArgumentException($"Called unwrap on a {nameof(None<T>)}!"),
            _ => Throw<T>(nameof(option))
        };
    
    public static T UnwrapOrDefault<T>(this Option<T> option, T @default) 
        where T : notnull =>
        option switch
        {
            Some<T> some => some.Value,
            None<T> => @default,
            _ => Throw<T>(nameof(option))
        };
    
    public static T UnwrapOrDefault<T>(this Option<T> option, Func<T> @default) 
        where T : notnull =>
        option switch
        {
            Some<T> some => some.Value,
            None<T> => @default(),
            _ => Throw<T>(nameof(option))
        };
}