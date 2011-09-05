using System;
using System.Collections.Generic;

namespace BoomSharp
{
	public interface IStorage
	{
		IDictionary<string, IDictionary<string, string>> Lists { get; set; }
		
		//Returns a list of tuples - list, key, value for a given key
		IList<Tuple<string, string, string>> GetItem(string key);
		
		//Returns a tuple - list, key, value for a given key
		Tuple<string, string, string> GetItem(string list, string key);
		
		IDictionary<string, string> GetList(string list);
		
		void AddList(string list);
		void AddItem(string list, string key, string value);
		
		void Initialize();
		void Save();
		
		bool IsInitialized { get; }
	}
}

