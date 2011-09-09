using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomSharp
{
	public class StringHelper
	{
		public static bool Equals(string s1, string s2)
		{
			return String.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool EqualsAny(string s1, params string[] strings)
		{
			foreach (string s in strings)
				if (StringHelper.Equals(s1, s))
					return true;

			return false;
		}

		public static bool EqualsAll(string s1, params string[] strings)
		{
			foreach (string s in strings)
				if (!StringHelper.Equals(s1, s))
					return false;

			return true;
		}
	}
}
