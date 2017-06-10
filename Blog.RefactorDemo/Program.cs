using System;

namespace Blog.RefactorDemo
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string text = "This is a long text to be searched";
			// A word to search within "text".
			string searchWord = "long";

			int lastIndex = GetIndexAfterFoundWord(text, searchWord);
			Console.WriteLine(lastIndex);
		}

		/// <summary>
		/// Find last index of found word within "text"
		/// </summary>
		/// <returns>
		/// text = "Hello, World!""
		/// searchWord = "World"
		/// then the result is 12 (right before "!")
		/// </returns>
		/// <remarks>Search "searchWord" within "text" using KMP (Knuth-Morris-Pratt) Algorithm.</remarks>
		private static int GetIndexAfterFoundWord(string text, string searchWord)
		{
			// Build Prefix KMP Table

			// Search searchWord using the table.
			
			// return the index after found word
		}
	}
}
