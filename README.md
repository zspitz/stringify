stringify
=========

Easy string concatenation for ``IEnumerable<T>``. Basically a wrapper for ``String.Join``, but allows using a ``StringBuilder``.
```csharp
var ints = new[] { 1, 2, 3, 4 };
result = ints.Stringify();
//returns "1234"

result = ints.Stringify("_");
//returns "1_2_3_4"

var numberMap = new Dictionary<int, string>() {
    {1,"one"},
    {2,"two"},
    {3,"three"},
    {4,"four"}
};
result = ints.Stringify(i => numberMap[i]);
//returns "onetwothreefour"

result = ints.Stringify("_", i => numberMap[i]);
//returns "one_two_three_four"

numberMap.Stringify(";",
    kvp => kvp.Key.ToString(),
    kvp => "=",
    kvp => kvp.Value
);
//returns "1=one;2=two;3=three;4=four"
```
