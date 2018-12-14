using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Message : BaseModel
    {
  //      public string SenderUsername
		//{
		//	get
		//	{
		//		return Sender != null ? Sender.Username : "";
		//	}
		//	set
		//	{
		//		if (Sender == null)
		//			Sender = new User();

		//		Sender.Username = value;
		//	}
		//}
  //      public string ReceiverUsername
		//{
		//	get
		//	{
		//		return Receiver != null ? Receiver.Username : "";
		//	}
		//	set
		//	{
		//		if (Receiver == null)
		//			Receiver = new User();

		//		Receiver.Username = value;
		//	}
		//}
        public string Text { get; set; }
		public bool Deleted { get; set; }

		#region helper properties
		public string Sender { get; set; }
		public string Receiver { get; set; }
		#endregion
	}
}
