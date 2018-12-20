using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        public UserRepository() : base()
        {

        }
        public UserRepository(IDbConnection con) : base(con)
        {

        }
        public void Add(User model)
        {
            try
            {
				string EncryptedPassword = EncryptPassword(model.Password);
				model.Password = EncryptedPassword;
                Dictionary<string, object> dictValues = GetValuesDictionary(model, false);

                DynamicParameters parms = new DynamicParameters(dictValues);

                StringBuilder query = new StringBuilder("INSERT INTO dbo.[Users] ");
                query.Append(GetInsertQuery(dictValues.Keys));

				_con.Execute(query.ToString(), parms);
			}
            catch
            {
                throw;
            }
        }

        //public void Delete(int id) //NOP

        //{
        //    try
        //    {
        //        string query = "DELETE dbo.[Users] WHERE Id = @Id";

        //        _con.Execute(query, new { Id = id });                
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
		public User DeleteByEmail(string email)
		{
			try
			{
				string query = "UPDATE dbo.[Users] SET Active='FALSE' WHERE Email = @Email";
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Email", email);
				return _con.Query<User>(query, parameters).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}


		public List<User> GetAll()
        {
            string query = "SELECT * FROM dbo.[Users] WHERE Active='TRUE' ORDER BY Username ASC";
            
            return _con.Query<User>(query.ToString()).ToList();
        }

		public User GetByEmail(string email)
		{
			try
			{
				string query = "SELECT * FROM dbo.[Users] where Email = @Email";
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Email", email);
				return _con.Query<User>(query, parameters).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}
        //public User GetById(int id)//NOP
        //      {
        //          try
        //          {
        //              string query = "SELECT * FROM dbo.[Users] where Id = @Id";
        //              DynamicParameters parameters = new DynamicParameters();
        //              parameters.Add("@Id", id);
        //              return _con.Query<User>(query, parameters).FirstOrDefault();
        //          }
        //          catch (Exception)
        //          {
        //              throw;
        //          }
        //      }
        public User GetAdmin()
        {
            try
            {
                string query = "SELECT * FROM dbo.[Users] where Role= 'Admin' AND Active='TRUE'";
                return _con.Query<User>(query).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public User GetByUsername(string username)
        {
            try
            {
                string query = "SELECT * FROM dbo.[Users] where Username= @Username AND Active='TRUE'";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                return _con.Query<User>(query, parameters).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Dictionary<string, object> GetValuesDictionary(User model, bool isForUpdate)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "@Username", model.Username },
                { "@Password", model.Password },
                { "@Role", model.Role },
                { "@Email", model.Email },
                { "@Name", model.Name },
                { "@Surname", model.Surname },
                { "@DateOfBirth", model.DateOfBirth },
                { "@Active", model.Active },
            };

            if (isForUpdate)
            {
                dict.Remove("@DateOfBirth");

            }


            return dict;
        }
        public Dictionary<string, object> GetValuesDictionary2(User model, bool isForUpdate)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "@Username", model.Username },
                { "@Password", model.Password },
                { "@Role", model.Role },
                { "@Email", model.Email },
                { "@Name", model.Name },
                { "@Surname", model.Surname },
                { "@DateOfBirth", model.DateOfBirth },
                { "@Active", model.Active },
            };

            if (isForUpdate)
            {
                dict.Remove("@DateOfBirth");
                dict.Remove("@Password");
            }


            return dict;
        }
        //    public Dictionary<string, object> GetValuesDictionary(User model, bool isForUpdate)
        //    {
        //        Dictionary<string, object> dict = new Dictionary<string, object>
        //        {
        //            { "@Username", model.Username },
        //            { "@Password", model.Password },
        //            { "@Role", model.Role },
        //{ "@Email", model.Email },
        //{ "@Name", model.Name },
        //{ "@Surname", model.Surname },
        //{ "@DateOfBirth", model.DateOfBirth },
        //{ "@Active", model.Active },
        ////{ "@DateTime", model.DateTime }
        //        };


        //            //if (isForUpdate)
        //            //{
        //            //    dict.Add("@ModifiedBy", model.ModifiedBy);
        //            //    dict.Add("@LastEditDate", DateTime.UtcNow);
        //            //}
        //            //else
        //            //{
        //            //    dict.Add("@CreatedBy", model.CreatedBy);
        //            //    dict.Add("@CreationDate", DateTime.UtcNow);
        //            //}

        //            return dict;
        //    }


        public void Update(User model)
        {
            try
            {
                Dictionary<string, object> dictValues = GetValuesDictionary(model, true);

                DynamicParameters parms = new DynamicParameters(dictValues);

                string query = String.Format($"UPDATE dbo.[Users] SET {GetUpdateQuery(dictValues.Keys)} WHERE Username = @Username");

                _con.Execute(query, parms);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Edit(User model)
        {
            try
            {
                Dictionary<string, object> dictValues = GetValuesDictionary2(model, true);

                DynamicParameters parms = new DynamicParameters(dictValues);

                string query = String.Format($"UPDATE dbo.[Users] SET {GetUpdateQuery(dictValues.Keys)} WHERE Username = @Username");

                _con.Execute(query, parms);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
