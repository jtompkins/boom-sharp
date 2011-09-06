using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BoomSharp
{
	public class ConsoleHelper
	{
		private static int TabWidth { get; set; }
		private static int IndentLevel { get; set; }
		
		static ConsoleHelper()
		{
			ConsoleHelper.TabWidth = 4;
		}

		#region General Console Method Wrappers

		public static void Write(string s, Color? c = null)
		{
			Console.Write(ConsoleHelper.GetIndentString() + s);
		}
		
		public static void WriteLine(string s, Color? c = null)
		{
			Console.WriteLine(ConsoleHelper.GetIndentString() + s);
		}
		
		public static void WriteList(IList<string> list, Color? c = null, char bullet = '*')
		{
			foreach (string s in list.OrderBy(i => i))
				Console.WriteLine(ConsoleHelper.GetIndentString() + bullet + " " + s);
		}
		
		public static void WriteDictionary(IDictionary<string, string> dict, int defaultPadding = 0, Color? keyColor = null, Color? valueColor = null)
		{
			if ((dict != null) && (dict.Count > 0))
			{
				int rightPadding = (defaultPadding == 0) ? (dict.Keys.Max(k => k.Length) + 4) : defaultPadding;

				foreach (string key in dict.Keys.OrderBy(k => k))
					Console.WriteLine(ConsoleHelper.GetIndentString() + (key + ": ").PadRight(rightPadding) + dict[key]);
			}
		}
		
		public static void WriteHorizontalRule(int width = 80)
		{
			Console.Write(ConsoleHelper.GetIndentString());
			
			for (int i = 0; i < width; i++)
				Console.Write("-");
			
			Console.Write("\n");
		}

		#endregion

		#region Boom-Sharp Specific Helpers

		public static void WriteAmbiguousKeysMessage(IList<Tuple<string, string, string>> items)
		{
			ConsoleHelper.WriteLine("Multiple keys found:");

			IList<string> msgs = items.Select(t => "In " + t.Item1 + " => " + t.Item2 + ": " + t.Item3).ToList();

			ConsoleHelper.IncreaseIndent();
			ConsoleHelper.WriteList(msgs);
			ConsoleHelper.ResetIndent();
		}

		public static void WriteKeyNotFoundMessage(string list, string key)
		{
			ConsoleHelper.WriteLine(key + " not found in " + list + ".");
		}

		public static void WriteKeyCopiedMessage(string value)
		{
			ConsoleHelper.WriteLine("Boom! We just copied " + value + " to the clipboard.");
		}

		public static void WriteListOpenedMessage(string list)
		{
			ConsoleHelper.WriteLine("Boom! We just opened all of " + list + " for you.");
		}

		public static void WriteKeyOpenedMessage(string list, string key)
		{
			ConsoleHelper.WriteLine("Boom! We just opened " + key + " for you.");
		}

		public static void WriteKeyAddedMessage(string list, string key, string value)
		{
			ConsoleHelper.WriteLine("Boom! " + key + " in " + list + " is " + value + ". Got it.");
		}

		public static void WriteListAddedMessage(string list)
		{
			ConsoleHelper.WriteLine("Boom! Created a new list called " + list + ".");
		}

		public static void WriteKeyRemovedMessage(string list, string key)
		{
			ConsoleHelper.WriteLine("Boom! " + key + " is gone forever from " + list + ".");
		}

		#endregion

		#region Indent Helpers

		private static string GetIndentString()
		{
			StringBuilder sb = new StringBuilder();
			
			for (int i = 0; i < ConsoleHelper.IndentLevel; i++)
				for (int j = 0; j < ConsoleHelper.TabWidth; j++)
					sb.Append(" ");
			
			return sb.ToString();
		}
		
		public static void IncreaseIndent()
		{
			ConsoleHelper.IndentLevel++;
		}
		
		public static void DecreaseIndent()
		{
			if (ConsoleHelper.IndentLevel > 0)
				ConsoleHelper.IndentLevel--;
		}
		
		public static void SetIndent(int indent)
		{
			ConsoleHelper.IndentLevel = indent;
		}
		
		public static void ResetIndent()
		{
			ConsoleHelper.IndentLevel = 0;
		}

		#endregion
	}
}

