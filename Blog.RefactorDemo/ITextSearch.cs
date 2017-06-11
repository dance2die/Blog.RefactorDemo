namespace Blog.RefactorDemo
{
	public interface ITextSearch
	{
		/// <summary>
		/// Find "text" with "searchWord" using index table "prefixTable"
		/// </summary>
		/// <param name="text">Text to be search</param>
		/// <param name="searchWord">Word sought</param>
		/// <returns>Found Indices. If none is found then returns an empty array</returns>
		int[] Find(string text, string searchWord);
	}
}