using BetterTeamsWebApp.Models.ViewModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetterTeamsWebApp.Controllers
{
    public class AdminController : Controller
    {
       
        public ActionResult Messages()
        {
            List<MessageVM> messages = new List<MessageVM>();
            MessageVM msg = new MessageVM();
            MessageRepository messageRepo = new MessageRepository();
            var msgs = messageRepo.GetAll().ToList();
            for (int i = 0; i < msgs.Count; i++)
            {
                msg = new MessageVM();
                msg.Id = msgs[i].Id;
                msg.Text = msgs[i].Text;
                msg.Sender = msgs[i].Sender;
                msg.Receiver = msgs[i].Receiver;
                msg.DateTime = msgs[i].DateTime.ToString();
                msg.Deleted = msgs[i].Deleted;
                messages.Add(msg);
            }
            return View(messages);
        } 
        public ActionResult EditMessage(string id)
        {

        }
        public ActionResult DeleteMessage(string id)
        {

        }
        public ActionResult Users()
        {

        }
        public ActionResult EditUser(string email)
        {

        }
        public ActionResult DeleteUser(string email)
        {

        }
        public ActionResult Rooms()
        {

        }
        public ActionResult EditRoom(string id)
        {

        }
        public ActionResult DeleteRoom(string id)
        {

        }
        public ActionResult Posts()
        {

        }
        public ActionResult EditPost(string id)
        {

        }
        public ActionResult DeletePost(string id)
        {

        }
    }
}