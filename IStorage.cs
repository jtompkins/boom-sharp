using System;
using System.Collections.Generic;

namespace boomsharp
{
	public interface IStorage
	{
		IDictionary<string, IDictionary<string, string>> Lists { get; set; }
		
		//Returns a list of tuples - list, key, value for a given key
		IList<Tuple<string, string, string>> GetItem(string key);
		
		//Returns a tuple - list, key, value for a given key
		Tuple<string, string, string> GetItem(string list, string key);
	}
}

