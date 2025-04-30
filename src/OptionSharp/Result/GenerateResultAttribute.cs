namespace OptionSharp.Result;

[AttributeUsage(AttributeTargets.Class)]
public class GenerateResultAttribute : Attribute
{
    public string Name { get; }

    public GenerateResultAttribute(string name)
    {
        Name = name;
    }
}