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
			{
				Console.WriteLine(ConsoleHelper.GetIndentString() + bullet + " " + s);
			}
		}
		
		public static void WriteDictionary(IDictionary<string, string> dict, Color? keyColor = null, Color? valueColor = null, bool balanceKeys = true)
		{
			int rightPadding = 0;
			
			if (dict.Count > 0)
			{
				if (balanceKeys)
					rightPadding = dict.Keys.Max(k => k.Length) + 4;
				
				IList<string> keys = dict.Keys.OrderBy(k => k).ToList();
				
				foreach (string key in keys)
				{
					Console.WriteLine(ConsoleHelper.GetIndentString() + (key + ": ").PadRight(rightPadding) + dict[key]);
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
	}
}

