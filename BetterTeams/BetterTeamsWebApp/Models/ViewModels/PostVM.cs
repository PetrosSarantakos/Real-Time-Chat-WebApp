﻿using Models;
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
        public User Sender { get; set; }
        public DateTime DateTime { get; set; }
        public Room Room { get; set; }
        public bool Deleted { get; set; }
    }
}