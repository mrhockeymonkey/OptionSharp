# OptionSharp

Just another option to choose from.

If you are looking for **stable**, production ready lib you are better off using something like `LanguageExt.Core`.

## Motivation

Most C# optional implementations use structs for better performance, which makes a lot of sense. 

This lib instead uses separate classes to model optional objects. The benefit here is that "if" C# gets discriminated unions,
then this model will fit in nicely and allow for idiomatic code using optional. If later a struct DU is introduced this lib could then use that.

So in short, at this point, its a gamble + fun side project to learn more about functional style in C# where performance is not a primary concern.
