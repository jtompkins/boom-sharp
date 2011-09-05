using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace BoomSharp
{
	public class Command
	{
		private IStorage Store { get; set; }
		
		public Command (IStorage s)
		{
			this.Store = s;
			
			if (!s.IsInitialized)
				s.Initialize();
		}
		
		public void RunCommand(string[] args)
		{
			if (args.Length > 0)
			{
				Queue<string> arguments = new Queue<string>(args);
				
				string command = arguments.Dequeue();				
				string major = (arguments.Count > 0) ? arguments.Dequeue() : null;
				string minor = (arguments.Count > 0) ? arguments.Dequeue() : null;
				
				switch (command.ToLower())
				{
					case "all":
					case "a":
						this.All();
					
						break;
						
					case "random":
					case "r":
						this.Random();
					
						break;
						
					case "echo":
					case "e":
						this.Echo(major, minor);
					
						break;
						
					case "open":
					case "o":
						this.Open(major, minor);
					
						break;
					
					case "help":
						this.Help();
					
						break;
					
					case "--version":
					case "v":
						this.Version();
					
						break;
					
					default:
						break;
				}
			}
		}
		
		public void All()
		{
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
		
		public void Echo(string major)
		{
		}
		
		public void Echo(string major, string minor)
		{
		}
		
		public void Open(string major)
		{
		}
		
		public void Open(string major, string minor)
		{
		}
	}
}