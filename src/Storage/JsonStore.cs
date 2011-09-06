using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace BoomSharp
{
	public class JsonStore : IStore
	{
		public static string JsonStorePath
		{
			get
			{
				string storePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
   										Environment.OSVersion.Platform == PlatformID.MacOSX)
    								? Environment.GetEnvironmentVariable("HOME")
    								: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
			
				storePath += Path.DirectorySeparatorChar + ".boom";
				
				return storePath;
			}
		}
		
		private IDictionary<string, IDictionary<string, string>> Lists { get; set; }
		
		public JsonStore ()
		{
			this.Lists = new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
			
			this.Initialize();
		}
		
		#region IStore implementation

		public string Name
		{
			get
			{
				return "json";
			}
		}

		public IList<Tuple<string, string, string>> GetItem(string key)
		{
			List<Tuple<string, string, string>> items = new List<Tuple<string, string, string>>();

			if (!String.IsNullOrEmpty(key))
			{ 
				foreach (KeyValuePair<string, IDictionary<string, string>> kvp in this.Lists)
				{
					if (kvp.Value.ContainsKey(key.ToLower()))
						items.Add(Tuple.Create(kvp.Key, key, kvp.Value[key]));
				}
			}
			
			return items;
		}

		public Tuple<string, string, string> GetItem (string list, string key)
		{
			if ((this.HasList(list)) && (this.HasKey(list, key)))
				return Tuple.Create(list, key, this.Lists[list][key]);
			
			return null;
		}

		public IDictionary<string, string> GetList (string list)
		{
			if (this.HasList(list))
				return this.Lists[list];
			
			return null;
		}
		
		public IDictionary<string, IDictionary<string, string>> All()
		{
			return this.Lists;
		}
		
		public void AddList (string list)
		{
			if (String.IsNullOrEmpty(list))
				return;

			if (!this.HasList(list))
				this.Lists.Add(list.ToLower(), new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
		}

		public void AddItem (string list, string key, string value)
		{
			if ((String.IsNullOrEmpty(list)) ||
				(String.IsNullOrEmpty(key)) ||
				(String.IsNullOrEmpty(value)))
				return;

			if (!this.HasList(list))
				this.AddList(list);
			
			if (!this.HasKey(list, key))
				this.Lists[list].Add(key.ToLower(), value.ToLower());
			else
				this.Lists[list][key] = value.ToLower();
		}
		
		public void RemoveList(string list)
		{
			if (this.HasList(list))
				this.Lists.Remove(list);
		}
		
		public void RemoveItem(string list, string key)
		{
			if (this.HasKey(list, key))
				this.Lists[list].Remove(key);
		}

		public bool HasList(string list)
		{
			if (!String.IsNullOrEmpty(list))
				return this.Lists.ContainsKey(list);
			
			return false;
		}

		public bool HasKey(string key)
		{
			if (!String.IsNullOrEmpty(key))
			{
				foreach (IDictionary<string, string> list in this.Lists.Values)
				{
					if (list.ContainsKey(key))
						return true;
				}
			}
				
			return false;
		}

		public bool HasKey (string list, string key)
		{
			if ((!String.IsNullOrEmpty(list)) && (!String.IsNullOrEmpty(key)) && (this.HasList(list)))
				return this.Lists[list].ContainsKey(key);

			return false;
		}

		public void ImportStore(IDictionary<string, IDictionary<string, string>> storeData)
		{
			return;
		}

		public void Initialize()
		{
			//if the configuration file doesn't exist, write out a skeleton for one.
			if (!File.Exists(JsonStore.JsonStorePath))
			{
				using (StreamWriter f = File.CreateText(JsonStore.JsonStorePath))
				{
					f.Write("{\"lists\":[]}");
				}
			}
			
			using (StreamReader f = new StreamReader(JsonStore.JsonStorePath))
			{
				string jsonData = f.ReadToEnd();
				
				if (!String.IsNullOrEmpty(jsonData))
				{
					//the ruby boom config JSON produces a weird data type
					var data = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, List<Dictionary<string, string>>>>>>(jsonData);
					
					if (data != null)
					{
						//we'll need to parse out this structure and simplify it for our use
						foreach (Dictionary<string, List<Dictionary<string, string>>> list in data["lists"])
						{
							KeyValuePair<string, List<Dictionary<string, string>>> kvp = list.FirstOrDefault();
							
							this.Lists.Add(kvp.Key, new Dictionary<string, string>());
							
							foreach (Dictionary<string, string> item in kvp.Value)
							{
								KeyValuePair<string, string> itemKvp = item.FirstOrDefault();
								
								this.Lists[kvp.Key].Add(itemKvp.Key, itemKvp.Value);
							}
						}
					}
				}
			}
		}

		public void Save()
		{
			//the JSON structure of the Boom data is pretty weird.  Instead of a dictionary
			//of dictionaries (i.e., an object containing other objects in JSON), it uses
			//a series of nested objects and lists.  to maintain compatibility, we need to
			//match that data structure before JSON serialization.
			
			//first, build the container object
			Dictionary<string, List<Dictionary<string, List<Dictionary<string, string>>>>> jsonList = new Dictionary<string, List<Dictionary<string, List<Dictionary<string, string>>>>>();
			
			jsonList.Add("lists", new List<Dictionary<string, List<Dictionary<string, string>>>>());
			
			//now, for each of our lists, we'll need to create a dictionary and add it to a list
			foreach(KeyValuePair<string, IDictionary<string, string>> kvp in this.Lists)
			{
				Dictionary<string, List<Dictionary<string, string>>> list = new Dictionary<string, List<Dictionary<string, string>>>();
				
				list.Add(kvp.Key, new List<Dictionary<string, string>>());
				
				foreach (KeyValuePair<string, string> itemKvp in this.Lists[kvp.Key])
				{
					Dictionary<string, string> itemDict = new Dictionary<string, string>();
					
					itemDict.Add(itemKvp.Key, itemKvp.Value);
					
					list[kvp.Key].Add(itemDict);
				}
				
				jsonList["lists"].Add(list);
			}
			
			using (StreamWriter f = File.CreateText(JsonStore.JsonStorePath))
			{
				f.Write(JsonConvert.SerializeObject(jsonList));
			}
		}

		#endregion
	}
}