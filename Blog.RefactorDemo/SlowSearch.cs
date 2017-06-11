using System.Collections.Generic;

namespace Blog.RefactorDemo
{
	public class SlowSearch : ITextSearch
	{
		public int[] Find(string text, string searchWord)
		{
			List<int> foundIndices = new List<int>();
			for (int i = 0; i < text.Length; i++)
			{
				int foundIndex = text.Substring(i).IndexOf(searchWord);
				foundIndices.Add(foundIndex);
			}

			return foundIndices.ToArray();
		}
	}
}