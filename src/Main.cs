using System;
using System.Collections.Generic;
using System.Configuration;

namespace BoomSharp
{
	public class BoomSharp
	{
		public const string VERSION = "1.0";
		
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

					BoomSharp.SwitchStore(c["backend"], true);
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

		public static IStore GetStore(string store, bool fallbackToDefault = false)
		{
			switch (store.ToLower())
			{
				case "gist":
					return new GistStore();

				case "json":
					return new JsonStore();

				default:
					if (fallbackToDefault)
						return new JsonStore();

					break;
			}

			return null;
		}

		public static bool SwitchStore(string newStore, bool fallbackToDefault = false)
		{
			IStore s = BoomSharp.GetStore(newStore, fallbackToDefault);

			if (s != null)
			{
				BoomConfig c = new BoomConfig();

				c["backend"] = s.Name;

				BoomSharp._Store = s;

				return true;
			}

			return false;
		}
		
		[STAThread] // for OLE
		public static void Main (string[] args)
		{
			//BoomSharp.Command.RunCommand(new string[] { "import", "json" });
			
			BoomSharp.Command.RunCommand(args);
		}
	}
}