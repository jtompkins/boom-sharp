using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BoomSharp
{
	public class CommandParser
	{
		public CommandParser()
		{
		}

		public bool RunCommand(string[] args)
		{
			if (args.Length > 0)
			{
				Queue<string> arguments = new Queue<string>(args);

				string command = arguments.Dequeue().ToLower();
				string major = (arguments.Count > 0) ? arguments.Dequeue().ToLower() : null;
				string minor = (arguments.Count > 0) ? arguments.Dequeue().ToLower() : null;

				//let's see if the first argument is one of our known commands.
				if ((command == "all") || (command == "a"))
					return this.All();
				else if ((command == "random") || (command == "r"))
					return this.Random(major);
				else if ((command == "echo") || (command == "e"))
					return (!String.IsNullOrEmpty(minor)) ? this.Echo(major, minor) : this.Echo(major);
				else if ((command == "open") || (command == "o"))
					return this.Open(major, minor);
				else if (command == "store")
					return this.ShowStore();
				else if (command == "switch")
					return this.SwitchStore(major);
				else if (command == "import")
					return this.ImportStore(major);
				else if (command == "help")
					return this.Help();
				else if ((command == "--version") || (command == "-v"))
					return this.Version();

				//first, see if we're dealing with a list
				if (BoomSharp.Store.HasList(command))
				{
					//let's see if we need to delete the list
					if (major == "delete")
						return this.RemoveList(command);

					//okay, now let's check to see if there are other arguments
					//if not, just show the items in the list
					if (String.IsNullOrEmpty(major))
						return this.ShowList(command);

					//if the 3rd argument is filled out and isn't "delete",
					//we're going to be either adding something or copying an item.
					//if the 3rd argument *is* "delete", we're removing an item.
					if (String.IsNullOrEmpty(minor))
						return this.CopyItem(command, major);
					else if (minor != "delete")
						return this.AddItem(command, major, minor);
					else
						return this.RemoveItem(command, major);
				}

				//okay, the command wasn't a list. Let's see if it's an 
				//item. if so, we're going to copy it.
				if (BoomSharp.Store.HasKey(command))
					return this.CopyItem(command);

				if (String.IsNullOrEmpty(major))
					return this.AddList(command);

				//last possible thing - we were passed all 3 arguments, but
				//the first one wasn't an existing list. in that case, we just
				//go ahead and create the list *and* the item.
				return this.AddItem(command, major, minor);
			}

			return this.Overview();
		}

		#region Commands

		public bool All()
		{
			ConsoleHelper.IncreaseIndent();

			IDictionary<string, IDictionary<string, string>> allLists = BoomSharp.Store.All();

			int keyPadding = allLists.SelectMany(d => d.Value).Select(kvp => kvp.Key.Length).Max() + 4;

			foreach (KeyValuePair<string, IDictionary<string, string>> list in allLists)
			{
				ConsoleHelper.WriteLine(list.Key, ConsoleColor.Cyan);

				ConsoleHelper.IncreaseIndent();

				ConsoleHelper.WriteDictionary(list.Value, keyPadding, ConsoleColor.Yellow);

				ConsoleHelper.DecreaseIndent();
			}

			return true;
		}

		public bool Overview()
		{
			if (BoomSharp.Store.All().Count > 0)
				return All();
			
			ConsoleHelper.WriteLine("You don't have anything yet! To start out, create a new list:");
			ConsoleHelper.WriteLine("  $ boom-sharp <list-name>");
			ConsoleHelper.WriteLine("And then add something to your list!");
			ConsoleHelper.WriteLine("  $ boom-sharp <list-name> <item-name> <item-value>");
			ConsoleHelper.WriteLine("You can then grab your new item:");
			ConsoleHelper.WriteLine("  $ boom-sharp <item-name>");

			return true;
		}

		public bool Help()
		{
			ConsoleHelper.IncreaseIndent();

			ConsoleHelper.WriteLine("- boom-sharp: help ---------------------------------------------------\n");

			ConsoleHelper.WriteLine("boom-sharp                          display high-level overview");
			ConsoleHelper.WriteLine("boom-sharp all                      show all items in all lists");
			ConsoleHelper.WriteLine("boom-sharp help                     this help text");
			ConsoleHelper.WriteLine("boom-sharp storage                  shows which storage backend you're using");
			ConsoleHelper.WriteLine("boom-sharp switch <storage>         switches to a different storage backend");
			ConsoleHelper.WriteLine("boom-sharp import <storage>         imports items from a different storage backend\n");
			
			ConsoleHelper.WriteLine("boom-sharp <list>                   create a new list");
			ConsoleHelper.WriteLine("boom-sharp <list>                   show items for a list");
			ConsoleHelper.WriteLine("boom-sharp <list> delete            deletes a list\n");
			
			ConsoleHelper.WriteLine("boom-sharp <list> <name> <value>    create a new list item");
			ConsoleHelper.WriteLine("boom-sharp <name>                   copy item's value to clipboard");
			ConsoleHelper.WriteLine("boom-sharp <list> <name>            copy item's value to clipboard");
			ConsoleHelper.WriteLine("boom-sharp open <list>              open the urls of all items in a list");
			ConsoleHelper.WriteLine("boom-sharp open <name>              open item's url in browser");
			ConsoleHelper.WriteLine("boom-sharp open <list> <name>       open item's url in browser for a list");
			ConsoleHelper.WriteLine("boom-sharp random                   open a random item's url in browser");
			ConsoleHelper.WriteLine("boom-sharp random <list>            open a random item's url for a list in browser");
			ConsoleHelper.WriteLine("boom-sharp echo <name>              echo the item's value without copying");
			ConsoleHelper.WriteLine("boom-sharp echo <list> <name>       echo the item's value without copying");
			ConsoleHelper.WriteLine("boom-sharp <list> <name> delete     deletes an item");

			return true;
		}

		public bool Version()
		{
			ConsoleHelper.WriteLine("You're running boom-sharp " + BoomSharp.VERSION + ". Congratulations!");

			return true;
		}

		public bool Random(string list)
		{
			//get the full dictionary to work on
			IDictionary<string, IDictionary<string, string>> lists = BoomSharp.Store.All();

			//choose a key for a random list

			string randomList = String.Empty;

			if ((!String.IsNullOrEmpty(list)) && (lists.ContainsKey(list)))
				randomList = list;
			else
				randomList = CommandParser.GetRandomItem<string>(lists.Keys);

			//sanity check, let's make sure the key is valid, and that the selected list has stuff in it
			if (lists.ContainsKey(randomList) && lists[randomList].Count > 0)
			{
				//choose a random key in the selected list
				string randomItem = CommandParser.GetRandomItem<string>(lists[randomList].Keys);

				//another sanity check here
				if (lists[randomList].ContainsKey(randomItem))
					return OpenKey(randomList, randomItem);

				return true;
			}

			return Random(list);
		}

		public bool SwitchStore(string store)
		{
			bool success = BoomSharp.SwitchStore(store);

			if (success)
			{
				ConsoleHelper.WriteLine("We've switched you over to " + BoomSharp.Store.Name + ".");

				return true;
			}

			ConsoleHelper.WriteLine("We couldn't find that storage engine. Check the name and try again.");

			return false;
		}

		public bool ShowStore()
		{
			ConsoleHelper.WriteLine("You're currently using " + BoomSharp.Store.Name + ".");

			return true;
		}

		public bool ImportStore(string store)
		{
			IStore s = BoomSharp.GetStore(store);

			if (s != null)
			{
				BoomSharp.Store.ImportStore(s.All());

				ConsoleHelper.WriteLine("Boom! We imported all items from " + store + " to " + BoomSharp.Store.Name + ".");

				return true;
			}

			ConsoleHelper.WriteLine("We couldn't find that storage engine. Check the name and try again.");

			return false;
		}

		public bool Echo(string key)
		{
			if (BoomSharp.Store.HasKey(key))
			{
				IList<Tuple<string, string, string>> items = BoomSharp.Store.GetItem(key);

				if (items != null)
				{
					if (items.Count > 1)
						ConsoleHelper.WriteAmbiguousKeysMessage(items);
					else
					{
						Tuple<string, string, string> item = items.FirstOrDefault();

						ConsoleHelper.WriteLine(item.Item3);
					}

					return true;
				}
			}

			return false;
		}

		public bool Echo(string list, string key)
		{
			if (BoomSharp.Store.HasKey(list, key))
			{
				Tuple<string, string, string> item = BoomSharp.Store.GetItem(list, key);

				if (item != null)
				{
					ConsoleHelper.WriteLine(item.Item3);

					return true;
				}
			}

			ConsoleHelper.WriteKeyNotFoundMessage(list, key);

			return false;
		}

		public bool Open(string major, string minor)
		{
			if (!String.IsNullOrEmpty(major))
			{
				if (BoomSharp.Store.HasList(major))
				{
					if (BoomSharp.Store.HasKey(minor))
						return this.OpenKey(major, minor);
					else
						return this.OpenList(major);
				}
				else if (BoomSharp.Store.HasKey(major))
					return this.OpenKey(major);
			}

			return false;
		}

		private bool OpenList(string list)
		{
			if (BoomSharp.Store.HasList(list))
			{
				IDictionary<string, string> l = BoomSharp.Store.GetList(list);

				foreach (KeyValuePair<string, string> kvp in l)
					System.Diagnostics.Process.Start(kvp.Value);

				ConsoleHelper.WriteListOpenedMessage(list);

				return true;
			}

			return false;
		}

		private bool OpenKey(string key)
		{
			IList<Tuple<string, string, string>> items = BoomSharp.Store.GetItem(key);

			if (items != null)
			{
				if (items.Count > 1)
					ConsoleHelper.WriteAmbiguousKeysMessage(items);
				else
				{
					Tuple<string, string, string> item = items.FirstOrDefault();

					System.Diagnostics.Process.Start(item.Item3);

					ConsoleHelper.WriteKeyOpenedMessage(item.Item1, item.Item2);
				}

				return true;
			}

			return false;
		}

		private bool OpenKey(string list, string key)
		{
			if (BoomSharp.Store.HasKey(list, key))
			{
				Tuple<string, string, string> item = BoomSharp.Store.GetItem(list, key);

				if (item != null)
				{
					System.Diagnostics.Process.Start(item.Item3);

					ConsoleHelper.WriteKeyOpenedMessage(item.Item1, item.Item2);

					return true;
				}
			}

			ConsoleHelper.WriteKeyNotFoundMessage(list, key);

			return false;
		}

		public bool ShowList(string list)
		{
			ConsoleHelper.IncreaseIndent();
			ConsoleHelper.WriteDictionary(BoomSharp.Store.GetList(list), 0, ConsoleColor.Yellow);
			ConsoleHelper.ResetIndent();

			return true;
		}

		public bool CopyItem(string key)
		{
			IList<Tuple<string, string, string>> items = BoomSharp.Store.GetItem(key);

			if (items != null)
			{
				if (items.Count > 1)
					ConsoleHelper.WriteAmbiguousKeysMessage(items);
				else
				{
					Tuple<string, string, string> item = items.FirstOrDefault();

					Clipboard.SetText(item.Item3);
					ConsoleHelper.WriteKeyCopiedMessage(item.Item3);
				}

				return true;
			}

			return false;
		}

		public bool CopyItem(string list, string key)
		{
			if (BoomSharp.Store.HasKey(list, key))
			{
				Tuple<string, string, string> item = BoomSharp.Store.GetItem(list, key);

				if (item != null)
				{
					Clipboard.SetText(item.Item3);
					ConsoleHelper.WriteKeyCopiedMessage(item.Item3);

					return true;
				}
			}

			ConsoleHelper.WriteKeyNotFoundMessage(list, key);

			return false;
		}

		public bool RemoveList(string list)
		{
			if (BoomSharp.Store.HasList(list))
			{
				ConsoleHelper.Write("You sure you want to delete everything in " + list + "? (y/n) ");

				if (Console.ReadLine() == "y")
				{
					BoomSharp.Store.RemoveList(list);
					BoomSharp.Store.Save();

					ConsoleHelper.Write("Boom!", ConsoleColor.Magenta);
					ConsoleHelper.Write(" Deleted all your ");
					ConsoleHelper.Write(list, ConsoleColor.Cyan);
					ConsoleHelper.WriteLine(".");
				}
				else
					ConsoleHelper.WriteLine("Just kidding then.");
			}

			return true;
		}

		public bool RemoveItem(string key)
		{
			if (BoomSharp.Store.HasKey(key))
			{
				IList<Tuple<string, string, string>> items = BoomSharp.Store.GetItem(key);

				if (items != null)
				{
					if (items.Count > 1)
						ConsoleHelper.WriteAmbiguousKeysMessage(items);
					else
					{
						Tuple<string, string, string> item = items.FirstOrDefault();

						BoomSharp.Store.RemoveItem(item.Item1, item.Item2);
						BoomSharp.Store.Save();

						ConsoleHelper.WriteKeyRemovedMessage(item.Item1, item.Item2);
					}

					return true;
				}
			}

			return false;
		}

		public bool RemoveItem(string list, string key)
		{
			if (BoomSharp.Store.HasKey(list, key))
			{
				Tuple<string, string, string> item = BoomSharp.Store.GetItem(list, key);

				if (item != null)
				{
					BoomSharp.Store.RemoveItem(list, key);
					BoomSharp.Store.Save();

					ConsoleHelper.WriteKeyRemovedMessage(item.Item1, item.Item2);

					return true;
				}
			}

			ConsoleHelper.WriteKeyNotFoundMessage(list, key);

			return false;
		}

		public bool AddList(string list)
		{
			BoomSharp.Store.AddList(list);
			BoomSharp.Store.Save();

			ConsoleHelper.WriteListAddedMessage(list);

			return true;
		}

		public bool AddItem(string list, string key, string value)
		{
			BoomSharp.Store.AddItem(list, key, value);
			BoomSharp.Store.Save();

			ConsoleHelper.WriteKeyAddedMessage(list, key, value);

			return true;
		}

		#endregion

		#region Utility Methods

		public static T GetRandomItem<T>(IEnumerable<T> list)
		{
			return list.ElementAt((new Random()).Next(list.Count()));
		}

		#endregion
	}
}