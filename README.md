stringify
=========

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


