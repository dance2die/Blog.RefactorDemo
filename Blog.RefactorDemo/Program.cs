using System;
using System.Collections.Generic;
using System.Linq;

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
			int j = 0;
			int i = j + 1;
			int[] T = Enumerable.Repeat(0, searchWord.Length).ToArray();
			T[0] = 0;

			while (i < searchWord.Length)
			{
				if (searchWord[i] == searchWord[j])
				{
					T[i] = j + 1;
					j++;
					i++;
				}
				else
				{
					while (j >= 1 && searchWord[j] != searchWord[i])
					{
						j = T[j - 1];
						if (j == 0) break;
					}

					if (searchWord[j] == searchWord[i])
						T[i] = j + 1;

					i++;
				}
			}


			// Search searchWord using the table.
			int wi = 0;  // index position for W
			int m = 0;  // index position for S
			List<int> found = new List<int>();

			while (m + wi < text.Length)
			{
				if (text[m + wi] == searchWord[wi])
				{
					wi++;
					if (wi == searchWord.Length)
					{
						found.Add(m);
						m = m + wi - T[wi - 1];
						wi = T[wi - 1];
					}
				}
				else
				{
					if (T[wi] == 0)
					{
						m = m + wi + 1;
						wi = 0;
					}
					else
					{
						m = m + wi;
						wi = (wi - 1) < 0 ? 0 : T[wi - 1];
					}
				}
			}

			// return the index after found word
			return found.First() + searchWord.Length;
		}
	}
}
