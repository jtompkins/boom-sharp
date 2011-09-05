using System;
using System.Collections.Generic;

namespace BoomSharp
{
	public class BoomSharp
	{
		private static IStorage GetStore()
		{
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