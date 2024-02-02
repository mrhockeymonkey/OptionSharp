using System.Diagnostics.CodeAnalysis;

namespace OptionSharp;

public static class ThrowHelpers
{
    [DoesNotReturn]
    internal static Option<T> ThrowOption<T>(string? paramName) => 
        throw new ArgumentOutOfRangeException(paramName);
    
    [DoesNotReturn]
    internal static Result<T, TErr> ThrowResult<T, TErr>(string? paramName) =>
        throw new ArgumentOutOfRangeException(paramName);
    
    [DoesNotReturn]
    internal static T Throw<T>(string? paramName) => 
        throw new ArgumentOutOfRangeException(paramName);
    
    [DoesNotReturn]
    internal static bool Throw(string? paramName) => 
        throw new ArgumentOutOfRangeException(paramName);
}