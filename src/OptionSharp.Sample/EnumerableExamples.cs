// ReSharper disable UnusedVariable

using OptionSharp.Option;

namespace OptionSharp.Sample;

using IntResult = Result<int, ErrMessage>;

public static class EnumerableExamples
{
    public static async Task AllExamples()
    {
        FilterAListOfOptions();
        FilterAListOfResults();
        await FilterAnAsyncListOfResults();
    }
    
    public static void FilterAListOfOptions()
    {
        List<Option<int>> options = [Some(1), None<int>(), Some(2), None<int>(), Some(3)];
        List<int> numbers = options.FilterMap(x => x).ToList();
    }

    public static void FilterAListOfResults()
    {
        List<Result<int, ErrMessage>> results = [Ok(1), Ok(2), Err<int>("NaN"), Ok(3)];
        List<int> numbers = results.Select(r => r.ToOption()).FilterMap(x => x).ToList();
    }

    public static async Task FilterAnAsyncListOfResults()
    {
        // uses System.Linq.Async
        List<int> numbers = await GetNumbersAsyncEnumerable()
            .Select(r => r.ToOption())
            .FilterMapAsyncEnumerable(x => x)
            .ToListAsync();
    }

    private static async IAsyncEnumerable<IntResult> GetNumbersAsyncEnumerable()
    {
        yield return Ok(2);
        await Task.Delay(TimeSpan.FromSeconds(1));
        yield return Ok(2);
        await Task.Delay(TimeSpan.FromSeconds(1));
        yield return Err<int>("NaN");
    }
}