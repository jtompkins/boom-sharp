using System;
using System.Collections.Generic;

namespace BoomSharp
{
	public interface IStorage
	{
		//Returns a list of tuples - list, key, value for a given key
		IList<Tuple<string, string, string>> GetItem(string key);
		
		//Returns a tuple - list, key, value for a given key
		Tuple<string, string, string> GetItem(string list, string key);
		
		IDictionary<string, string> GetList(string list);
		
		void AddList(string list);
		void AddItem(string list, string key, string value);
		
		bool HasList(string list);
		bool HasKey(string key);
		bool HasKey(string list, string key);
		
		void Initialize();
		void Save();
		
		bool IsInitialized { get; }
	}
}

