using System.Collections.Generic;
using System.Linq;

namespace Blog.RefactorDemo
{
	public class KmpSearch : ITextSearch
	{
		/// <summary>
		/// Find "text" with "searchWord" using index table "prefixTable"
		/// </summary>
		/// <param name="text">Text to be search</param>
		/// <param name="searchWord">Word sought</param>
		/// <returns>Found Indices. If none is found then returns an empty array</returns>
		public int[] Find(string text, string searchWord)
		{
			int[] prefixTable = BuildPrefixTable(searchWord);

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
		private int[] BuildPrefixTable(string searchWord)
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

	}
}