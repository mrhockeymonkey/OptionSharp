using static OptionSharp.Constructors;
using static OptionSharp.ThrowHelpers;

namespace OptionSharp;

public static class ConversionExtensions
{
    public static Result<T, TErr> ToResult<T, TErr>(this Option<T> option, Func<TErr> error) =>
        option switch
        {
            Some<T> some => Ok<T, TErr>(some.Value),
            None<T> => Err<T, TErr>(error()),
            _ => ThrowResult<T, TErr>(nameof(option))
        };
    
    public static Option<T> ToOption<T, TErr>(this Result<T, TErr> result) =>
        result switch
        {
            Ok<T, TErr> ok => Some(ok.Value),
            Err<T, TErr> err => None<T>(),
            _ => ThrowOption<T>(nameof(result))
        };
}