using System.Runtime.CompilerServices;

namespace OptionSharp.SourceGen.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        //https://github.com/VerifyTests/Verify.SourceGenerators#initialize
        VerifySourceGenerators.Initialize();
    }
}