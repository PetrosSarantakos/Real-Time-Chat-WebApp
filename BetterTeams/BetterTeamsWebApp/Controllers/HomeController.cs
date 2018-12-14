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
            string sender = "admin";
            string receiver = "petros_sa";
            UserRepository ur = new UserRepository();
            User U1 = ur.GetByUsername(sender);
            User U2= ur.GetByUsername(receiver);

            List<Message> messages = new List<Message>();

            MessageRepository messageRepo = new MessageRepository();
            messages = messageRepo.GetBySenderRecveiverUsername(U1, U2);
            return View(messages);
        }

        [HttpPost]
        public ActionResult Index(MesssageVM messageVM)
        {
            MessageRepository messageRepo = new MessageRepository();
            Message message = new Message{
                Sender=messageVM.Sender,
                Receiver=messageVM.Receiver,
                Text=messageVM.Message,
                DateTime=DateTime.Now,
                Deleted=messageVM.Deleted
            };

            messageRepo.Add(message);
            return View(message);
        }


        public ActionResult About()
        {
            return View();
        }
    }
}