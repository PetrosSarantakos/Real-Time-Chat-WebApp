using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Post : BaseModel
	{
		public int SenderEmail { get; set; }
		public string PostText { get; set; }
		public bool Deleted { get; set; }
		public string Room { get; set; }
		public User Sender { get; set; }
	}
}
