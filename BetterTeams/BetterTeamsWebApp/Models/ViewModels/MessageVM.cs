using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTeamsWebApp.Models.ViewModels
{
    public class MessageVM
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }
        public bool Deleted { get; set; }
    }
}