using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Post : BaseModel
	{
		//public int SenderEmail { get; set; }
		public string PostText { get; set; }
		public bool Deleted { get; set; }
		public string Room { get; set; }
		//public string SenderEmail
		//{
		//	get
		//	{
		//		return Sender != null ? Sender.Email : "";
		//	}
		//	set
		//	{
		//		if (Sender == null)
		//			Sender = new User();

		//		Sender.Email = value;
		//	}
		//}
		public string Sender { get; set; }
		
	}
}
