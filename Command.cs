using System;

namespace boomsharp
{
	public class Command
	{
		private IStorage Store { get; set; }
		
		public Command (IStorage s)
		{
		}
	}
}

