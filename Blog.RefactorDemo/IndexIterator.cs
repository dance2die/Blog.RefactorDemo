using System.Linq;

namespace Blog.RefactorDemo
{
	public class IndexIterator
	{
		private readonly ITextSearch _textSearch;

		public IndexIterator(ITextSearch textSearch)
		{
			_textSearch = textSearch;
		}

		public int GetIndexAfterFoundWord4(string text, string searchWord)
		{
			int[] found = _textSearch.Find(text, searchWord);
			return found.First() + searchWord.Length;
		}
	}
}