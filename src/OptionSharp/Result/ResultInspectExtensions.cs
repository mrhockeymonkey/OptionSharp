using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Result;

public static class ResultInspectExtensions
{
    public static Result<T, TErr> Inspect<T, TErr>(this Result<T, TErr> result, Action<T> inspect)
    {
        if (result is Ok<T, TErr> o) inspect(o.Value);
        
        return result switch
        {
            Ok<T, TErr> ok => ok,
            Err<T, TErr> err => err,
            _ => ThrowResult<T, TErr>(nameof(result))
        };
    }
    
    public static Result<T, TErr> InspectErr<T, TErr>(this Result<T, TErr> result, Action<TErr> inspect)
    {
        if (result is Err<T, TErr> e) inspect(e.Error);
        
        return result switch
        {
            Ok<T, TErr> ok => ok,
            Err<T, TErr> err => err,
            _ => ThrowResult<T, TErr>(nameof(result))
        };
    }
    
    public static async Task<Result<T, TErr>> InspectAsync<T, TErr>(this Task<Result<T, TErr>> result, Action<T> inspect) => 
        (await result).Inspect(inspect);

    public static async Task<Result<T, TErr>> InspectErrAsync<T, TErr>(this Task<Result<T, TErr>> result, Action<TErr> inspect) => 
        (await result).InspectErr(inspect);
}