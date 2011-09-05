using System;
using System.Collections.Generic;

namespace boomsharp
{
	public class MainClass
	{		
		public IDictionary<string, IDictionary<string, string>> Booms { get; private set; }
		
		private bool HasList(string name)
		{
			if (this.Booms.ContainsKey(name))
			{
				if (this.Booms[name] == null)
					this.Booms[name] = new Dictionary<string, string>();
				
				return true;
			}
			
			return false;
		}
		
		private bool HasKey(string list, string key)
		{
			if (this.HasList(list))
			{
				if (this.Booms[list].ContainsKey(key))
					return true;
			}
			
			return false;
		}
		
		private bool HasKey(string key)
		{
			foreach (string list in this.Booms.Keys)
			{
				if (this.Booms[list].ContainsKey(key))
					return true;
			}
			
			return false;
		}
		
		private void AddList(string list)
		{
			if (!this.Booms.ContainsKey(list))
				this.Booms.Add(new Dictionary<string, string>());
		}
		
		private void AddKey(string list, string key, string value)
		{
			if (!this.Booms.ContainsKey(list))
				this.AddList(list);
			
			if (!this.Booms[list].ContainsKey(key))
				this.Booms[list].Add(key, value);
		}
				
		public static void Main (string[] args)
		{
			/*
			 * Command Line Cases:
			 * 		1. Return item
			 * 			a. Return multiple items if ambigious
			 * 			b. Copy item to clipboard
			 * 			c. Print item to console
			 * 		2. Return items in list
			 * 		3. Add list
			 * 		4. Remove item
			 * 			a. Choose item if ambigious
			 * 		5. Remove list
			 * 		6. Add item
			 */
			
			if (args.Length == 1)
			{
				if (this.HasKey(args[0]))
				{
					//return key
				}
				else if (this.HasList(args[0]))
				{
					//return list items
				}
				else
				{
					//create list
				}
			}
			else if (args.Length == 2)
			{
				if (args[1].ToLower() == "delete")
				{
					if (this.HasKey(args[0]))
					{
						//remove item
					}
					else if (this.HasList(args[0]))
					{
						//remove list
					}
				}
				else if ((args[0].ToLower() == "echo") && (this.HasKey(args[1])))
				{
					//print the value of the second key
				}
				else if (args[0].ToLower() == "open")
				{
					if (this.HasList(args[1]))
					{
						//open all the keys in the list in the browser
					}
					else if (this.HasKey(args[1]))
					{
					}
					
					//open the second key in the browser
				}
				else
				{
					if (this.HasList(args[0]))
					{
						if (this.Booms[args[0]].ContainsKey(args[1]))
						{
							//return key
						}
					}
				}
			}
		}
	}
}