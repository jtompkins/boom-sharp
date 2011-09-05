using System;

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
		}
		
		public void All()
		{
		}
		
		public void Overview()
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

