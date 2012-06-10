using System;
using System.Collections.Generic;

namespace BoomSharp
{
	public interface IStore
	{
		//Returns a list of tuples - list, key, value for a given key
		IList<Tuple<string, string, string>> GetItem(string key);
		
		//Returns a tuple - list, key, value for a given key
		Tuple<string, string, string> GetItem(string list, string key);
		
		IDictionary<string, IDictionary<string, string>> All();
		
		IDictionary<string, string> GetList(string list);
		
		void AddList(string list);
		void AddItem(string list, string key, string value);
		
		void RemoveList(string list);
		void RemoveItem(string list, string key);
		
		bool HasList(string list);
		bool HasKey(string key);
		bool HasKey(string list, string key);

		string Name { get; }

		void ImportStore(IDictionary<string, IDictionary<string, string>> storeData);
		
		void Initialize();
		void Save();
	}
}

