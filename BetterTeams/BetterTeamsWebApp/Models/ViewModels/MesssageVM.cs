using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterTeamsWebApp.Models.ViewModels
{
    public class MesssageVM
    {
        public int Id { get; set; }
        //public User Sender { get; set; }
        //public User Receiver { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public bool Deleted { get; set; }
    }
}