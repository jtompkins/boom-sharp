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
		
		public void RunCommand(string[] args)
		{
			if (args.Length > 0)
			{
				Queue<string> arguments = new Queue<string>(args);
				
				string command = arguments.Dequeue().ToLower();				
				string major = (arguments.Count > 0) ? arguments.Dequeue().ToLower() : null;
				string minor = (arguments.Count > 0) ? arguments.Dequeue().ToLower() : null;
				
				//let's see if the first argument is one of our known commands.
				if ((command == "all") || (command == "a"))
				{
					this.All();
					return;
				}
				else if ((command == "random") || (command == "r"))
				{
					this.Random();
					return;
				}
				else if ((command == "echo") || (command == "e"))
				{
					this.Echo(major, minor);
					return;
				}
				else if ((command == "open") || (command == "o"))
				{
					this.Open(major, minor);
					return;
				}
				else if (command == "help")
				{
					this.Help();
					return;
				}
				else if ((command == "--version") || (command == "-v"))
				{
					this.Version();
					return;
				}
				
				//first, see if we're dealing with a list
				if (BoomSharp.Store.HasList(command))
				{
					//let's see if we need to delete the list
					if (major == "delete")
					{
						this.RemoveList(command);
						return;
					}
					
					//okay, now let's check to see if there are other arguments
					//if not, just show the items in the list
					if (String.IsNullOrEmpty(major))
					{
						this.ShowList(command);
						return;
					}
					
					//if the 3rd argument is filled out and isn't "delete",
					//we're going to be either adding something or copying an item.
					//if the 3rd argument *is* "delete", we're removing an item.
					if (String.IsNullOrEmpty(minor))
					{
						this.CopyItem(command, major);
						return;
					}
					else if (minor != "delete")
					{
						this.AddItem(command, major, minor);
						return;
					}
					else
					{
						this.RemoveItem(command, major);
						return;
					}
				}
				
				//okay, the command wasn't a list. Let's see if it's an 
				//item. if so, we're going to copy it.
				if (BoomSharp.Store.HasKey(command))
				{
					this.CopyItem(command);
					return;
				}
				
				//last possible thing - we were passed all 3 arguments, but
				//the first one wasn't an existing list. in that case, we just
				//go ahead and create the list *and* the item.
				this.AddItem(command, major, minor);
			}
		}
		
		public void All()
		{
			ConsoleHelper.IncreaseIndent();
			
			foreach (KeyValuePair<string, IDictionary<string, string>> kvp in BoomSharp.Store.All())
			{
				ConsoleHelper.WriteLine(kvp.Key);
				
				ConsoleHelper.IncreaseIndent();
				
				ConsoleHelper.WriteDictionary(kvp.Value);
				
				ConsoleHelper.DecreaseIndent();
			}
		}
		
		public void Overview()
		{
		}
		
		public void Help()
		{
		}
		
		public void Version()
		{
		}
		
		public void Random()
		{
		}
		
		public void Echo(string key)
		{
		}
		
		public void Echo(string list, string key)
		{
		}
		
		public void Open(string key)
		{
		}
		
		public void Open(string list, string key)
		{
		}
		
		public void ShowList(string list)
		{
			ConsoleHelper.IncreaseIndent();
			ConsoleHelper.WriteDictionary(BoomSharp.Store.GetList(list), null, null, true);
			ConsoleHelper.ResetIndent();
		}
		
		public void CopyItem(string key)
		{
			IList<Tuple<string, string, string>> items = BoomSharp.Store.GetItem(key);
			
			if (items != null)
			{
				if (items.Count > 1)
					PrintMultipleItems(items);
				else
				{
					Tuple<string, string, string> item = items.FirstOrDefault();
					
					Clipboard.Clear();
					Clipboard.SetText(item.Item3);
					                  
					ConsoleHelper.WriteLine("Boom! We just copied " + item.Item3 + " to the clipboard.");
				}
			}
		}
		
		public void CopyItem(string list, string key)
		{
			Tuple<string, string, string> item = BoomSharp.Store.GetItem(list, key);
			
			if (item != null)
			{
				Clipboard.Clear();
				Clipboard.SetText(item.Item3);
				
				ConsoleHelper.WriteLine("Boom! We just copied " + item.Item3 + " to the clipboard.");
			}
			else
			{
				ConsoleHelper.WriteLine(key + " not found in " + list + ".");
			}
		}
		
		public void RemoveList(string list)
		{
			if (!String.IsNullOrEmpty(list))
			{
				ConsoleHelper.Write("You sure you want to delete everything in " + list + "? (y/n) ");
				
				if (Console.ReadLine() == "y")
				{
					BoomSharp.Store.RemoveList(list);
					BoomSharp.Store.Save();
					
					ConsoleHelper.WriteLine("Boom! Deleted all your " + list + ".");
				}
				else
					ConsoleHelper.WriteLine("Just kidding then.");
			}
		}
		
		public void RemoveItem(string key)
		{
			IList<Tuple<string, string, string>> items = BoomSharp.Store.GetItem(key);
			
			if (items != null)
			{
				if (items.Count > 1)
					PrintMultipleItems(items);
				else
				{
					Tuple<string, string, string> item = items.FirstOrDefault();
					
					BoomSharp.Store.RemoveItem(item.Item1, item.Item2);
					BoomSharp.Store.Save();
					
					ConsoleHelper.WriteLine("Boom! " + item.Item2 + " is gone forever.");
				}
			}
		}
		
		public void RemoveItem(string list, string key)
		{
			Tuple<string, string, string> item = BoomSharp.Store.GetItem(list, key);
			
			if (item != null)
			{
				BoomSharp.Store.RemoveItem(list, key);
				BoomSharp.Store.Save();
				
				ConsoleHelper.WriteLine("Boom! " + item.Item2 + " is gone forever.");
			}
			else
			{
				ConsoleHelper.WriteLine(key + " not found in " + list + ".");
			}
		}
		
		public void AddList(string list)
		{
			BoomSharp.Store.AddList(list);
			BoomSharp.Store.Save();
			
			ConsoleHelper.WriteLine("Boom! Created a new list called " + list + ".");
		}
		
		public void AddItem(string list, string key, string value)
		{
			BoomSharp.Store.AddItem(list, key, value);
			BoomSharp.Store.Save();
			
			ConsoleHelper.WriteLine("Boom! " + key + " in " + list + " is " + value + ". Got it.");
		}
		
		private void PrintMultipleItems(IList<Tuple<string, string, string>> items)
		{
			ConsoleHelper.WriteLine("Multiple keys found:");
					
			IList<string> msgs = items.Select(t => "In " + t.Item1 + " => " + t.Item2 + ": " + t.Item3).ToList();
			
			ConsoleHelper.IncreaseIndent();
			ConsoleHelper.WriteList(msgs);
			ConsoleHelper.ResetIndent();
		}
	}
}