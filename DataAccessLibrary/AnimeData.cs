using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
	public class AnimeData : IAnimeData
	{
		private readonly ISqlDataAccess _db;

		public AnimeData(ISqlDataAccess db)
		{
			_db = db;
		}

		public Task<List<AnimeModel>> GetSelectedAnime()
		{
			string sql = "select * from dbo.Anime";

			return _db.LoadData<AnimeModel, dynamic>(sql, new { });
		}

		public Task InsertAnime(AnimeModel anime)
		{
			string sql = @"insert into dbo.Anime (Id)
						   values (@Id);";

			return _db.SaveData(sql, anime);
		}

		public Task DeleteAnime(AnimeModel anime)
		{
			string sql = @"delete from dbo.Anime where Id = (@Id)";

			return _db.SaveData(sql, anime);
		}
	}
}
