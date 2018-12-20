using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Room:BaseModel
	{
		public string Name { get; set; }
		public string CreatorEmail { get; set; }
		public string Creator { get; set; }
	}
}
