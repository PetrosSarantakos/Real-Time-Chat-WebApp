
using System;
using System.Security.Cryptography;
using System.Text;

namespace Models
{
	
	public class User : BaseModel
    {
		public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string DateOfBirth { get; set; }
		public bool Active { get; set; }
        public string Role { get; set; }

      
    }
}
