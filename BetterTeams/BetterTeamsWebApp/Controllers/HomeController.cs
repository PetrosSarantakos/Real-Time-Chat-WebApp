using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterTeamsWebApp.Models.ViewModels;
using Repository;
using Models;

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
            MessageRepository messageRepo = new MessageRepository();
            Message message = new Message{
                SenderUsername=messageVM.Sender,
                ReceiverUsername=messageVM.Receiver,
                Text=messageVM.Message,
                DateTime=DateTime.Now,
                Deleted=messageVM.Deleted
            };

            messageRepo.Add(message);
            return View();
        }


        public ActionResult About()
        {
            return View();
        }
    }
}