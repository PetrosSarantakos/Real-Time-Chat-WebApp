using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterTeamsWebApp.Models.ViewModels;
using Repository;
using Models;
using System.Threading.Tasks;
using Reporitory;

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
            
            MessageRepository messageRepo = new MessageRepository();

            var msges = messageRepo.GetBySenderRecveiverUsername(U1, U2);
            for (int i = 0; i < msges.Count; i++)
            {
                messages[i].Id = msges[i].Id;
                messages[i].Message = msges[i].Text;
                messages[i].Sender = msges[i].Sender;
                messages[i].Receiver = msges[i].Receiver;
                messages[i].DateTime = msges[i].DateTime;
                messages[i].Deleted = msges[i].Deleted;
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
                Text = messageVM.Message,
                DateTime = DateTime.Now,
                Deleted = messageVM.Deleted
            };

            messageRepo.Add(message);

            return Json(messageVM, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetMessages(List<MessageVM> List)
        {

            string sender = "panospap";
            string receiver = "test";
            UserRepository ur = new UserRepository();
            User U1 = ur.GetByUsername(sender);
            User U2 = ur.GetByUsername(receiver);

            List<MessageVM> messages = new List<MessageVM>();

            MessageRepository messageRepo = new MessageRepository();

            var msges = messageRepo.GetBySenderRecveiverUsername(U1, U2);
            for (int i = 0; i < msges.Count; i++)
            {
                messages[i].Id = msges[i].Id;
                messages[i].Message = msges[i].Text;
                messages[i].Sender = msges[i].Sender;
                messages[i].Receiver = msges[i].Receiver;
                messages[i].DateTime = msges[i].DateTime;
                messages[i].Deleted = msges[i].Deleted;
            }
            return Json(msges, JsonRequestBehavior.AllowGet);
        }
    }
}