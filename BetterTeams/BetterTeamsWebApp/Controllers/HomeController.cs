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
    [Authorize]
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
            User U2 = ur.GetByUsername(receiver);

            List<MessageVM> messages = new List<MessageVM>();
            MessageVM msg = new MessageVM();
            
            MessageRepository messageRepo = new MessageRepository();

            var msges = messageRepo.GetBySenderRecveiverUsername(U1, U2);
            for (int i = 0; i < msges.Count-1; i++)
            {
                msg.Id = msges[i].Id;
                msg.Text = msges[i].Text;
                msg.Sender = msges[i].Sender;
                msg.Receiver = msges[i].Receiver;
                msg.DateTime = msges[i].DateTime;
                msg.Deleted = msges[i].Deleted;
;               messages.Add(msg);
            }
            return View(messages);
        }


        public ActionResult About()
        {
            return View();
        }


        public JsonResult PostMessage(MessageVM messageVM)
        {
            MessageRepository messageRepo = new MessageRepository();
            Message message = new Message
            {
                Sender = messageVM.Sender,
                Receiver = messageVM.Receiver,
                Text = messageVM.Text,
                DateTime = DateTime.Now,
                Deleted = messageVM.Deleted
            };

            messageRepo.Add(message);

            return Json(messageVM, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetMessages(List<MessageVM> List)
        {

            string Username1 = ViewBag.CurrentUser;
            string Username2 = ViewBag.OtherUser;
            UserRepository Userdb = new UserRepository();
            User U1 = Userdb.GetByUsername(Username1);
            User U2 = Userdb.GetByUsername(Username2);

            List<MessageVM> messages = new List<MessageVM>();
            MessageVM msg = new MessageVM();

            MessageRepository messageRepo = new MessageRepository();

            var msges = messageRepo.GetBySenderRecveiverUsername(U1, U2);
            for (int i = 0; i < msges.Count; i++)
            {
                msg.Id = msges[i].Id;
                msg.Text = msges[i].Text;
                msg.Sender = msges[i].Sender;
                msg.Receiver = msges[i].Receiver;
                msg.DateTime = msges[i].DateTime;
                msg.Deleted = msges[i].Deleted;
                messages.Add(msg);
            }
            return Json(msges, JsonRequestBehavior.AllowGet);
        }
    }
}