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

		public static void Write(string s, ConsoleColor? c = null)
		{
			if (c.HasValue)
				Console.ForegroundColor = c.Value;

			Console.Write(ConsoleHelper.GetIndentString() + s);

			if (c.HasValue)
				Console.ResetColor();
		}

		public static void WriteLine(string s, ConsoleColor? c = null)
		{
			if (c.HasValue)
				Console.ForegroundColor = c.Value;

			Console.WriteLine(ConsoleHelper.GetIndentString() + s);

			if (c.HasValue)
				Console.ResetColor();
		}

		public static void WriteList(IList<string> list, ConsoleColor? c = null, char bullet = '*')
		{
			if (c.HasValue)
				Console.ForegroundColor = c.Value;

			foreach (string s in list.OrderBy(i => i))
				Console.WriteLine(ConsoleHelper.GetIndentString() + bullet + " " + s);

			if (c.HasValue)
				Console.ResetColor();
		}

		public static void WriteDictionary(IDictionary<string, string> dict, int defaultPadding = 0, ConsoleColor? keyColor = null, ConsoleColor? valueColor = null)
		{
			if ((dict != null) && (dict.Count > 0))
			{
				int rightPadding = (defaultPadding == 0) ? (dict.Keys.Max(k => k.Length) + 4) : defaultPadding;

				foreach (string key in dict.Keys.OrderBy(k => k))
				{
					Console.Write(ConsoleHelper.GetIndentString());

					if (keyColor.HasValue)
						Console.ForegroundColor = keyColor.Value;

					Console.Write((key + ": ").PadRight(rightPadding));

					if (keyColor.HasValue)
						Console.ResetColor();

					if (valueColor.HasValue)
						Console.ForegroundColor = valueColor.Value;

					Console.WriteLine(dict[key]);

					if (valueColor.HasValue)
						Console.ResetColor();
				}
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
			ConsoleHelper.Write(key, ConsoleColor.Yellow);
			ConsoleHelper.Write(" not found in ");
			ConsoleHelper.Write(list, ConsoleColor.Cyan);
			ConsoleHelper.WriteLine(".");
		}

		public static void WriteKeyCopiedMessage(string value)
		{
			ConsoleHelper.Write("Boom!", ConsoleColor.Magenta);
			ConsoleHelper.Write(" We just copied ");
			ConsoleHelper.Write(value, ConsoleColor.Green);
			ConsoleHelper.WriteLine(" to the clipboard.");
		}

		public static void WriteListOpenedMessage(string list)
		{
			ConsoleHelper.Write("Boom!", ConsoleColor.Magenta);
			ConsoleHelper.Write(" We just opened all of ");
			ConsoleHelper.Write(list, ConsoleColor.Cyan);
			ConsoleHelper.WriteLine(" for you.");
		}

		public static void WriteKeyOpenedMessage(string list, string key)
		{
			ConsoleHelper.Write("Boom!", ConsoleColor.Magenta);
			ConsoleHelper.Write(" We just opened ");
			ConsoleHelper.Write(key, ConsoleColor.Yellow);
			ConsoleHelper.WriteLine(" for you.");
		}

		public static void WriteKeyAddedMessage(string list, string key, string value)
		{
			ConsoleHelper.Write("Boom! ", ConsoleColor.Magenta);
			ConsoleHelper.Write(key, ConsoleColor.Yellow);
			ConsoleHelper.Write(" in ");
			ConsoleHelper.Write(list, ConsoleColor.Cyan);
			ConsoleHelper.Write(" is ");
			ConsoleHelper.Write(value, ConsoleColor.Green);
			ConsoleHelper.WriteLine(". Got it.");
		}

		public static void WriteListAddedMessage(string list)
		{
			ConsoleHelper.Write("Boom!", ConsoleColor.Magenta);
			ConsoleHelper.Write(" Created a new list called ");
			ConsoleHelper.Write(list, ConsoleColor.Cyan);
			ConsoleHelper.WriteLine(".");
		}

		public static void WriteKeyRemovedMessage(string list, string key)
		{
			ConsoleHelper.Write("Boom! ", ConsoleColor.Magenta);
			ConsoleHelper.Write(key, ConsoleColor.Yellow);
			ConsoleHelper.Write(" is gone forever from ");
			ConsoleHelper.Write(list, ConsoleColor.Cyan);
			ConsoleHelper.WriteLine(".");
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

