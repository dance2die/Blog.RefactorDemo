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

			int nextIndex = GetIndexAfterFoundWord(text, searchWord);
			Console.WriteLine("Result of GetIndexAfterFoundWord  = " + nextIndex);

			nextIndex = GetIndexAfterFoundWord2(text, searchWord);
			Console.WriteLine("Result of GetIndexAfterFoundWord2 = " + nextIndex);

			nextIndex = GetIndexAfterFoundWord3(text, searchWord);
			Console.WriteLine("Result of GetIndexAfterFoundWord3 = " + nextIndex);

			nextIndex = GetIndexAfterFoundWord4(text, searchWord, new KmpSearch());
			Console.WriteLine("Result of GetIndexAfterFoundWord4 using KmpSearch  = " + nextIndex);
			nextIndex = GetIndexAfterFoundWord4(text, searchWord, new SlowSearch());
			Console.WriteLine("Result of GetIndexAfterFoundWord4 using SlowSearch = " + nextIndex);
		}

		private static int GetIndexAfterFoundWord4(string text, string searchWord, ITextSearch textSearch)
		{
			int[] found = textSearch.Find(text, searchWord);
			return found.First() + searchWord.Length;
		}

		private static int GetIndexAfterFoundWord3(string text, string searchWord)
		{
			KmpSearch kmpSearch = new KmpSearch();
			int[] found = kmpSearch.Find(text, searchWord);
			return found.First() + searchWord.Length;
		}

		private static int GetIndexAfterFoundWord2(string text, string searchWord)
		{
			int[] prefixTable = BuildPrefixTable(searchWord);
			int[] found = SearchByKmp(text, searchWord, prefixTable);
			return found.First() + searchWord.Length;
		}

		/// <summary>
		/// Search "text" with "searchWord" using index table "prefixTable"
		/// </summary>
		/// <param name="text">Text to be search</param>
		/// <param name="searchWord">Word sought</param>
		/// <param name="prefixTable">KMP Table</param>
		/// <returns>Found Indices. If none is found then returns an empty array</returns>
		private static int[] SearchByKmp(string text, string searchWord, int[] prefixTable)
		{
			int i = 0;  // index position for searchWord
			int m = 0;  // index position for text
			List<int> found = new List<int>();

			while (m + i < text.Length)
			{
				if (text[m + i] == searchWord[i])
				{
					i++;
					if (i == searchWord.Length)
					{
						found.Add(m);
						m = m + i - prefixTable[i - 1];
						i = prefixTable[i - 1];
					}
				}
				else
				{
					if (prefixTable[i] == 0)
					{
						m = m + i + 1;
						i = 0;
					}
					else
					{
						m = m + i;
						i = (i - 1) < 0 ? 0 : prefixTable[i - 1];
					}
				}
			}

			return found.ToArray();
		}

		/// <summary>
		/// Build Prefix table using Algorithm in YouTube video
		/// https://youtu.be/GTJr8OvyEVQ
		/// </summary>
		/// <remarks>
		/// The result table contains a bit different values compared to using algorithm in Wikipedia.
		/// The result table contains values bigger than 0 while Wikipedia version contains -1 as a starting index.
		/// <see cref="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm#Description_of_pseudocode_for_the_table-building_algorithm"/>
		/// </remarks>
		private static int[] BuildPrefixTable(string searchWord)
		{
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

			return T;
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
			int wi = 0;  // index position for searchWord
			int m = 0;  // index position for text
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
