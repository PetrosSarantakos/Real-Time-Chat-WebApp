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
    public class MessageRepository : BaseRepository, IRepository<Message>
    {
        #region ReadLevel
        public enum MessageReadLevel
        {
            Normal,
            WithSenderReceiver
        }

        public MessageReadLevel ReadLevel { get; set; } = MessageReadLevel.Normal;
        #endregion

        public MessageRepository() : base()
        {

        }
        public MessageRepository(IDbConnection con) : base(con)
        {

        }
        public void Add(Message model)
        {
            try
            {
                Dictionary<string, object> dictValues = GetValuesDictionary(model, false);

                DynamicParameters parms = new DynamicParameters(dictValues);

                StringBuilder query = new StringBuilder("INSERT INTO dbo.[Messages] ");
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
                string query = "UPDATE dbo.[Messages] SET Deleted='TRUE' WHERE Id = @Id";

                _con.Execute(query, new { Id = id });
            }
            catch (Exception)
            {
                throw;
            }
        }

		public void DeleteBySenderOrReceiverEmail(string email) //TODO:CHECK
		{
			try
			{
				string query = "UPDATE dbo.[Messages] SET Deleted='TRUE' WHERE Sender = @Email OR Receiver = @Email";

				_con.Execute(query, new { Email = email });
			}
			catch (Exception)
			{
				throw;
			}
		}

        public List<Message> GetAll()
        {
            StringBuilder query = new StringBuilder("SELECT * FROM dbo.[Messages]");

            return _con.Query<Message>(query.ToString()).ToList();
        }
        //public List<Message> GetAll() //TODO:CHECK (Μπορει να μην το χρειαστουμε)
        //{
        //    StringBuilder query = new StringBuilder("SELECT * FROM dbo.[Messages] M");

        //    if (ReadLevel == MessageReadLevel.WithSenderReceiver)
        //    {
        //        query.Append(" JOIN dbo.[Users] S ON M.Sender = S.Username");
        //        query.Append(" JOIN dbo.[Users] R ON M.Receiver = R.Username");
        //    }

        //    query.Append(" ORDER BY M.Id ASC");

        //    if (ReadLevel == MessageReadLevel.WithSenderReceiver)
        //        return _con.Query<Message, User, User, Message>(query.ToString(), (m, s, r) =>
        //        {
        //            m.Sender = s;
        //            m.Receiver = r;

        //            return m;
        //        }).ToList();
        //    else
        //        return _con.Query<Message>(query.ToString()).ToList();
        //}
        //public List<Message> GetAll2(User sender, User receiver) //TODO:CHECK (Μπορει να μην το χρειαστουμε)
        //{
        //    StringBuilder query = new StringBuilder("SELECT * FROM dbo.[Messages] M");
        //    if (ReadLevel == MessageReadLevel.WithSenderReceiver)
        //    {
        //        DynamicParameters parameters = new DynamicParameters();
        //        query.Append(" JOIN dbo.[Users] S ON M.Sender = S.Username");
        //        parameters.Add("S.Username", sender.Username);
        //        query.Append(" JOIN dbo.[Users] R ON M.Receiver = R.Username");
        //        parameters.Add("r.Username", receiver.Username);
        //    }
        //    query.Append(" ORDER BY M.Id ASC");
        //    if (ReadLevel == MessageReadLevel.WithSenderReceiver)
        //        return _con.Query<Message, User, User, Message>(query.ToString(), (m, s, r) =>
        //        {
        //            m.Sender = s;
        //            m.Receiver = r;
        //            return m;
        //        }).ToList();
        //    else
        //        return _con.Query<Message>(query.ToString()).ToList();
        //}

        public Message GetById(int id)
        {
            try
            {
                string query = "SELECT * FROM dbo.[Messages] where Id = @Id AND Deleted='FALSE'";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                return _con.Query<Message>(query, parameters).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

		public List<Message> GetByReceiverId(int receiverId)
		{
			try
			{
				string query = "SELECT * FROM dbo.[Messages] where ReceiverId = @ReceiverId AND Deleted='FALSE'";
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@ReceiverId", receiverId);

				return _con.Query<Message>(query, parameters).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}
        public List<Message> GetBySenderRecveiverUsername(User sender, User receiver) //TODO: MATCH WITH FRONT!!!
        {
            try
            {
                string query = "SELECT * FROM dbo.[Messages] WHERE (Sender = @SenderUsername AND Receiver = @ReceiverUsername) OR (Sender = @ReceiverUsername AND Receiver = @SenderUsername) AND Deleted='FALSE' ORDER BY Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SenderUsername", sender.Username);
                parameters.Add("@ReceiverUsername", receiver.Username);
                
                return _con.Query<Message>(query, parameters).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, object> GetValuesDictionary(Message model, bool isForUpdate)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "@Receiver", model.Receiver },
                { "@Sender", model.Sender },
               // { "@Subject", model.Subject },
                { "@Text", model.Text },
				{ "@Deleted", model.Deleted },
				{ "@DateTime", model.DateTime }
            };

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

        public void Update(Message model)
        {
            try
            {
                Dictionary<string, object> dictValues = GetValuesDictionary(model, true);

                DynamicParameters parms = new DynamicParameters(dictValues);
                parms.Add("@Id", model.Id);

                string query = String.Format($"UPDATE dbo.[Messages] SET {GetUpdateQuery(dictValues.Keys)} WHERE Id = @Id");

                _con.Execute(query, parms);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
