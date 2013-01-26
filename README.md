stringify
=========

<<<<<<< HEAD
Easy string concatenation for IEnumerable&lt;T>

<code>
var keyValueItems = new Dictionary&lt;string, string&gt;() {
     {"key1","value1"},
     {"key2","value2"},
     {"key3","value3"}
};
		///	string result =
		///		keyValueItems.Stringify(
		///			kvp => kvp.Key,
		///			kvp => " ",
		///			kvp => kvp.Value
		///		},";");


=======
Easy string concatenation extension method for ``IEnumerable<T>``.
```csharp
var ints = new[] { 1, 2, 3, 4 };
result = ints.Stringify();
//returns "1234"
```
Basically a wrapper for ``String.Join``:
```csharp
result = ints.Stringify("_");
//returns "1_2_3_4"
```
but allows passing in a transformation function (similar to other LINQ aggregation operators):
```csharp
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
```
Passing multiple transformation functions will append each result to an internal ``StringBuilder``
```csharp
numberMap.Stringify(";",
    kvp => kvp.Key.ToString(),
    kvp => "=",
    kvp => kvp.Value
);
//returns "1=one;2=two;3=three;4=four"
```
>>>>>>> e374d7a2dfc97bca1db0f3163b801b4003c8bc03
