using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTeamsWebApp.Models.ViewModels
{
    public class UserVM
    {
        public string Email { get; set; }
        public string Username { get; set; }
		public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string DateOfBirth { get; set; }
        public bool Active { get; set; }

        public HashSet<string> ConnectionId { get; set; }
    }
}