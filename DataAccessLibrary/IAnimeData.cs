namespace DataAccessLibrary
{
	public interface IAnimeData
	{
		Task DeleteAnime(AnimeModel anime);
		Task<List<AnimeModel>> GetSelectedAnime();
		Task InsertAnime(AnimeModel anime);
	}
}