using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class PostRepository : BaseRepository, IRepository<Post>
	{
		public PostRepository() : base()
		{

		}
		public PostRepository(IDbConnection con) : base(con)
		{

		}
		public void Add(Post model)
		{
			try
			{
				Dictionary<string, object> dictValues = GetValuesDictionary(model, false);

				DynamicParameters parms = new DynamicParameters(dictValues);

				StringBuilder query = new StringBuilder("INSERT INTO dbo.[Posts] ");
				query.Append(GetInsertQuery(dictValues.Keys));

				_con.Execute(query.ToString(), parms);
			}
			catch
			{
				throw;
			}
		}

		public void Delete(int id)
		{
			try
			{
				string query = "UPDATE dbo.[Posts] SET Deleted='TRUE' WHERE Id = @Id";

				_con.Execute(query, new { Id = id });
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void DeleteBySenderUsername(string username)
		{
			try
			{
				string query = "UPDATE dbo.[Posts] SET Deleted='TRUE' WHERE Sender = @Sender";

				_con.Execute(query, new { Sender = username });
			}
			catch (Exception)
			{
				throw;
			}
		}

		//public void DeleteBySenderOrReceiverId(int id)
		//{
		//	try
		//	{
		//		string query = "DELETE dbo.Post WHERE SenderId = @UserId OR ReceiverId = @UserId";

		//		_con.Execute(query, new { UserId = id });
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		public List<Post> GetAll()
		{
			string query = "SELECT * FROM dbo.[Posts] WHERE Deleted='FALSE' ORDER BY Id ASC";

			return _con.Query<Post>(query.ToString()).ToList();
		}

		public Post GetById(int id)
		{
			try
			{
				string query = "SELECT * FROM dbo.[Posts] WHERE Deleted='FALSE' AND Id = @Id";
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Id", id);
				return _con.Query<Post>(query, parameters).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public List<Post> GetByRoom(string room)
		{
			try
			{
				string query = "SELECT * FROM dbo.[Posts] WHERE Deleted='FALSE' AND Room = @Room";
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Room", room);

				return _con.Query<Post>(query, parameters).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public Dictionary<string, object> GetValuesDictionary(Post model, bool isForUpdate)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>
			{
				{ "@Sender", model.Sender },
               // { "@Subject", model.Subject },
                { "@PostText", model.PostText },
				{ "@Deleted", model.Deleted },
				{ "@Room", model.Room },
				{ "@DateTime", model.DateTime }
            };
			//if (!isForUpdate)
			//{
			//	dict.Add("DateTime", DateTime.UtcNow);
			//}
				
			//if (isForUpdate)
			//{
			//    dict.Add("@ModifiedBy", model.ModifiedBy);
			//    dict.Add("@LastEditDate", DateTime.UtcNow);
			//}
			//else
			//{
			//    dict.Add("@CreatedBy", model.CreatedBy);
			//    dict.Add("@CreationDate", DateTime.UtcNow);
			//}

			return dict;
		}

		public void Update(Post model)
		{
			try
			{
				Dictionary<string, object> dictValues = GetValuesDictionary(model, true);

				DynamicParameters parms = new DynamicParameters(dictValues);
				parms.Add("@Id", model.Id);

				string query = String.Format($"UPDATE dbo.[Posts] SET {GetUpdateQuery(dictValues.Keys)} WHERE Id = @Id");

				_con.Execute(query, parms);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
