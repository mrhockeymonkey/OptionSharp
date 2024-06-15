using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Result;

public static class ResultConditionalExtensions
{
    public static bool IsOk<T, TErr>(this Result<T, TErr> result) =>
        result switch
        {
            Ok<T, TErr> => true,
            Err<T, TErr> => false,
            _ => Throw(nameof(result))
        };
    
    public static bool IsOkAnd<T, TErr>(this Result<T, TErr> result, Func<T, bool> predicate) =>
        result switch
        {
            Ok<T, TErr> ok => predicate(ok.Value),
            Err<T, TErr> => false,
            _ => Throw(nameof(result))
        };
    
    public static bool IsErr<T, TErr>(this Result<T, TErr> result) =>
        result switch
        {
            Ok<T, TErr> => false,
            Err<T, TErr> => true,
            _ => Throw(nameof(result))
        };
    
    public static bool IsErrAnd<T, TErr>(this Result<T, TErr> result, Func<TErr, bool> predicate) =>
        result switch
        {
            Ok<T, TErr> => false,
            Err<T, TErr> err => predicate(err.Error),
            _ => Throw(nameof(result))
        };
}