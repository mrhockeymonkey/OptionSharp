using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Result;

public static class ResultUnwrapExtensions
{
    /// <summary>
    /// Returns the underlying value from Ok<T, TErr> or throws!
    /// Usage is discouraged, prefer to use Reduce, Match or other means to handle Ok and Err cases.
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TErr"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static T Unwrap<T, TErr>(this Result<T, TErr> result)
        where T : notnull
        where TErr : notnull =>
        result switch
        {
            Ok<T, TErr> some => some.Value,
            Err<T, TErr> => throw new ArgumentException($"Called unwrap on a {nameof(Err<T, TErr>)}!"),
            _ => Throw<T>(nameof(result))
        };
    
    public static T UnwrapOrDefault<T, TErr>(this Result<T, TErr> result, T @default) =>
        result switch
        {
            Ok<T, TErr> ok => ok.Value,
            Err<T, TErr> => @default,
            _ => Throw<T>(nameof(result))
        };
    
    public static T UnwrapOrDefault<T, TErr>(this Result<T, TErr> result, Func<T> @default) =>
        result switch
        {
            Ok<T, TErr> ok => ok.Value,
            Err<T, TErr> => @default(),
            _ => Throw<T>(nameof(result))
        };
}