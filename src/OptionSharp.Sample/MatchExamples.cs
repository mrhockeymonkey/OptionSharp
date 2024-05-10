namespace OptionSharp.Sample;

public static class MatchExamples
{
    public static void AllExamples()
    {
        MatchActions();
        MatchFunc();
    }

    public static void MatchActions()
    {
        List <Option<string>> things = [Some("thing"), None<string>(), Some("other thing")];
        foreach (var option in things)
        {
            option.Match(
                Console.WriteLine, 
                () => Console.WriteLine("nothing"));
        }
    }
    
    public static void MatchFunc()
    {
        List <Option<string>> fruits = [Some("apple"), None<string>(), Some("orange")];

        var journal = fruits
            .Select(f => 
                f.Match(fruit => $"I ate an {fruit}", () => "I skipped a meal"))
            .ToList();
        
        foreach (var j in journal)
        {
            Console.WriteLine(j);
        }
    }
}