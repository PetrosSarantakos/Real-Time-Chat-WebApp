using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterTeamsWebApp.Models.ViewModels;

namespace BetterTeamsWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {                        
            return View();
        }

        [HttpPost]
        public ActionResult Index(MesssageVM messageVM)
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }
    }
}