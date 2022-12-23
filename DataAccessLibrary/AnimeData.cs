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

		public Task<List<int>> GetSelectedAnime()
		{
			string sql = "select * from dbo.Anime";

			return _db.LoadData<int, dynamic>(sql, new { });
		}

		public Task InsertAnime(int id)
		{
			string sql = @"insert into dbo.Anime (id)
						   values (@id);";

			return _db.SaveData(sql, id);
		}

		public Task DeleteAnime(int id)
		{
			string sql = @"delete from dbo.Anime where id = (@id)";

			return _db.SaveData(sql, id);
		}
	}
}
