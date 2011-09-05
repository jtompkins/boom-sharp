using System;
using System.Collections.Generic;
using System.Configuration;

namespace BoomSharp
{
	public class BoomSharp
	{
		private static IStore _Store { get; set; }
		private static BoomConfig _Config { get; set; }
		private static CommandParser _Command { get; set; }
		
		public static IStore Store
		{
			get
			{
				if (BoomSharp._Store == null)
				{
					BoomConfig c = new BoomConfig();
					
					switch (c["backend"].ToLower())
					{
						case "gist":
							BoomSharp._Store = new GistStore();
						
							break;
						
						default:
							BoomSharp._Store = new GistStore();
						
							break;
					}
				}
				
				return BoomSharp._Store;
			}
		}
		
		public static BoomConfig Config
		{
			get
			{
				if (BoomSharp._Config == null)
					BoomSharp._Config = new BoomConfig();
				
				return BoomSharp._Config;
			}
		}
		
		public static CommandParser Command
		{
			get
			{
				if (BoomSharp._Command == null)
					BoomSharp._Command = new CommandParser();
				
				return BoomSharp._Command;
			}
		}
				
		public static void Main (string[] args)
		{
			BoomSharp.Command.RunCommand(args);
		}
	}
}