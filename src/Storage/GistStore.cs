using System;
using System.Collections.Generic;

namespace BoomSharp
{
	public class GistStore : IStore
	{
		public GistStore()
		{
		}
	
		private IDictionary<string, IDictionary<string, string>> Lists { get; set; }
		private string GistId { get; set; }
		
		#region IStorage implementation
		
		public IDictionary<string, IDictionary<string, string>> All()
		{
			throw new NotImplementedException();
		}
		
		public IList<Tuple<string, string, string>> GetItem (string key)
		{
			throw new NotImplementedException ();
		}

		public Tuple<string, string, string> GetItem (string list, string key)
		{
			throw new NotImplementedException ();
		}

		public IDictionary<string, string> GetList (string list)
		{
			throw new NotImplementedException ();
		}

		public void AddList (string list)
		{
			throw new NotImplementedException ();
		}

		public void AddItem (string list, string key, string value)
		{
			throw new NotImplementedException ();
		}
		
		public void RemoveList(string list)
		{
		}
		
		public void RemoveItem(string list, string key)
		{
		}

		public bool HasList (string list)
		{
			throw new NotImplementedException ();
		}

		public bool HasKey (string key)
		{
			throw new NotImplementedException ();
		}

		public bool HasKey (string list, string key)
		{
			throw new NotImplementedException ();
		}
		
		public void Initialize()
		{
		}
		
		public void Save()
		{
			throw new NotImplementedException ();
		}
		
		#endregion
	}
}

