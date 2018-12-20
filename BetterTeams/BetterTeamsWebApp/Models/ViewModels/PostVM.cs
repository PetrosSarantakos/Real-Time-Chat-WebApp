using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTeamsWebApp.Models.ViewModels
{
    public class PostVM
    {
        public int Id { get; set; }
        public string PostText { get; set; }
        public string Sender { get; set; }
        public DateTime DateTime { get; set; }
        public string Room { get; set; }
        public bool Deleted { get; set; }
    }
}