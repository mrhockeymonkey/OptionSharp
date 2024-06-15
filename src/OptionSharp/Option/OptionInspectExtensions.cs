using static OptionSharp.ThrowHelpers;

namespace OptionSharp.Option;

public static class OptionInspectExtensions
{
    public static Option<T> Inspect<T>(this Option<T> option, Action<T> inspect)
        where T : notnull
    {
        if (option is Some<T> s) inspect(s.Value);
        
        return option switch
        {
            Some<T> some => some,
            None<T> none => none,
            _ => ThrowOption<T>(nameof(option))
        };
    }
    
    public static async Task<Option<T>> InspectAsync<T>(this Task<Option<T>> option, Action<T> inspect)
        where T : notnull =>
        (await option).Inspect(inspect);
}