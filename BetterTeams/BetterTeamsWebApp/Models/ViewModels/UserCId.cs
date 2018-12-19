using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTeamsWebApp.Models.ViewModels
{
    public class UserCId
    {
        public string Username { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}