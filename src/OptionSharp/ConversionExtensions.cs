using static OptionSharp.Constructors;
using static OptionSharp.ThrowHelpers;

namespace OptionSharp;

public static class ConversionExtensions
{
    public static Result<T, TErr> ToResult<T, TErr>(this Option<T> option, Func<TErr> orError) 
        where T : notnull 
        where TErr : notnull =>
        option switch
        {
            Some<T> some => Ok<T, TErr>(some.Value),
            None<T> => Err<T, TErr>(orError()),
            _ => ThrowResult<T, TErr>(nameof(option))
        };
    
    public static Option<T> ToOption<T, TErr>(this Result<T, TErr> result) 
        where T : notnull =>
        result switch
        {
            Ok<T, TErr> ok => Some(ok.Value),
            Err<T, TErr> => None<T>(),
            _ => ThrowOption<T>(nameof(result))
        };

    public static Option<T> ToOption<T, TErr>(this Result<T, TErr> result, Action<TErr> onError) 
        where T : notnull
    {
        if (result is Err<T, TErr> e)
            onError(e.Error);

        return result.ToOption();
    }
}