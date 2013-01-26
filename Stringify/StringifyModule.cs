/*
 * Copyright (c) 2013 Zev Spitz
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, 
 * including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished 
 * to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR 
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 * DEALINGS IN THE SOFTWARE.
 */

using System.Collections.Generic;
using System.Text;

namespace System.Linq {
	public static class StringifyModule {
		/// <summary>
		/// Converts/concatenates a sequence into a single string. Makes use of <see cref="String.Join"/> if possible
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="source"/></typeparam>
		/// <param name="source">Sequence of elements</param>
		/// <param name="separator">Separator string between elements</param>
		/// <param name="selectors">A transform function to be applied to each element.
		/// Or, multiple functions which will be applied successively to each element, and the results of which will be appended in order to generate the final string.
		/// </param>
		/// <example>
		/// This sample shows how to use Stringify to append multiple strings without concatenating into a new string, by passing in multiple delegates.
		/// <code>
		/// var keyValueItems = new Dictionary&lt;string, string&gt;() {
		///		{"key1","value1"},
		///		{"key2","value2"},
		///		{"key3","value3"}
		///	};
		///
		///	string result =
		///		keyValueItems.Stringify(";",
		///			kvp => kvp.Key,
		///			kvp => " ",
		///			kvp => kvp.Value
		///		);
		///	Console.WriteLine(result);
		/// </code>
		/// </example>
		/// <returns>Generated string</returns>
		public static string Stringify<T>(
				this IEnumerable<T> source,
				string separator = "",
				params Func<T, string>[] selectors) {

			separator = separator ?? "";
			selectors =
				selectors
					.Where(fn => fn != null)
					.ToArray();

			if (selectors.Length == 0) {
				return String.Join(separator, source);
			}
			if (selectors.Length == 1) {
				return String.Join(separator, source.Select(selectors[0]));
			}

			//TODO create separate overload that explicitly uses a StringBuilder
			//	String.Concat
			//	String.Join

			//use a StringBuilder
			var sb = new StringBuilder();
			bool hasSeparator = separator != "";
			bool firstIteration = true;
			foreach (var item in source) {
				if (hasSeparator) {
					if (firstIteration) {
						firstIteration = false;
					} else {
						sb.Append(separator);
					}
				}
				foreach (var fn in selectors) {
					sb.Append(fn(item));
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Converts/concatenates a sequence into a single string. Makes use of <see cref="String.Join"/>
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="source"/></typeparam>
		/// <param name="source">Sequence of elements</param>
		/// <param name="selectors">A transform function to be applied to each element.
		/// Or, multiple functions which will be applied successively to each element, and the results of which will be appended in order to generate the final string.
		/// </param>
		/// <returns>Generated string</returns>
		public static string Stringify<T>(
				this IEnumerable<T> source,
				params Func<T, string>[] selectors) {
			return source.Stringify("", selectors);
		}

		//TODO when passing multiple selectors, allow returning non-string types, and call ToString automatically
	}
}