using System;
using System.Collections.Generic;
using System.Configuration;

namespace BoomSharp
{
	public class BoomSharp
	{
		private static IStorage GetStore()
		{
			string backend = ConfigurationSettings.AppSettings["Backend"];
			
			switch (backend.ToLower())
			{
				case "gist":
				default:
					return new GistStorage();
			}
			
			throw new NotImplementedException();
		}
		
		public static void Main (string[] args)
		{
			IStorage store =  BoomSharp.GetStore();
			
			if (store != null)
			{
				Command c = new Command(store);
				
				c.RunCommand(args);
			}
		}
	}
}