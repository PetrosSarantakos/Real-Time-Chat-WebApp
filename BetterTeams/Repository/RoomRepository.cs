using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace Repository
{
	public class RoomRepository : BaseRepository, IRepository<Room>
	{
		public RoomRepository() : base()
		{

		}
		public RoomRepository(IDbConnection con) : base(con)
		{

		}
		public void Add(Room model)
		{
			try
			{
				Dictionary<string, object> dictValues = GetValuesDictionary(model, false);

				DynamicParameters parms = new DynamicParameters(dictValues);

				StringBuilder query = new StringBuilder("INSERT INTO dbo.[Rooms] ");
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
				string query = "DELETE dbo.[Rooms] WHERE Id = @Id";

				_con.Execute(query, new { Id = id });
			}
			catch (Exception)
			{
				throw;
			}
		}

		public List<Room> GetAll()
		{
			string query = "SELECT * FROM dbo.[Rooms] ORDER BY Name ASC";

			return _con.Query<Room>(query.ToString()).ToList();
		}

		public Room GetById(int id)
		{
			try
			{
				string query = "SELECT * FROM dbo.[Rooms] where Id = @Id";
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Id", id);
				return _con.Query<Room>(query, parameters).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public Room GetByName(string name)
		{
			try
			{
				string query = "SELECT * FROM dbo.[Rooms] where Name = @Name";
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Name", name);
				return _con.Query<Room>(query, parameters).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}
		//public List<string> GetAllUsersEmailInRoom(int id)
		//{
		//	string query = "SELECT Email FROM dbo.[RoomsUsers] where RoomId = @RoomId";
		//	DynamicParameters parameters = new DynamicParameters();
		//	parameters.Add("@RoomId", id);
		//	return _con.Query<string>(query, parameters).ToList();
				
		//}

		//public List<int> GetAllRoomsByEmail(string email)
		//{
		//	string query = "SELECT Id FROM dbo.[RoomsUsers]where Email = @Email";
		//	DynamicParameters parameters = new DynamicParameters();
		//	parameters.Add("@Email", email);
		//	return _con.Query<int>(query, parameters).ToList();
		//}



        public List<string> GetAllUsernamesInARoom(int id)
        {
            string query = "SELECT dbo.[Users].Username FROM dbo.[Users] INNER JOIN dbo.[RoomsUsers] ON dbo.[RoomsUsers].Email = dbo.[Users].Email WHERE dbo.[RoomsUsers].RoomId = @Id";
            DynamicParameters parameters= new DynamicParameters();
            parameters.Add("@Id", id);
            return _con.Query<string>(query, parameters).ToList();
        }


        public List<string> GetNameRoomsByEmail (string email)
        {
            string query = "SELECT dbo.[Rooms].Name FROM dbo.[Rooms] INNER JOIN dbo.[RoomsUsers] ON dbo.[Rooms].Id = dbo.[RoomsUsers].RoomId WHERE dbo.[RoomsUsers].Email=@Email";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            return _con.Query<string>(query, parameters).ToList();
        }




		public Dictionary<string, object> GetValuesDictionary(Room model, bool isForUpdate)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>
			{
				{ "@Name", model.Name },
				{ "@Creator", model.CreatorEmail },
			};
			if (!isForUpdate)
			{
				dict.Add("DateTime", DateTime.UtcNow);
			}
			return dict;
		}
		public void Update(Room model)
		{
			try
			{
				Dictionary<string, object> dictValues = GetValuesDictionary(model, true);
				DynamicParameters parms = new DynamicParameters(dictValues);
				parms.Add("@Id", model.Id);
				string query = String.Format($"UPDATE dbo.[Rooms] SET {GetUpdateQuery(dictValues.Keys)} WHERE Id = @Id");
				_con.Execute(query, parms);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}