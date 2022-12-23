namespace DataAccessLibrary
{
	public interface IAnimeData
	{
		Task DeleteAnime(int id);
		Task<List<int>> GetSelectedAnime();
		Task InsertAnime(int id);
	}
}