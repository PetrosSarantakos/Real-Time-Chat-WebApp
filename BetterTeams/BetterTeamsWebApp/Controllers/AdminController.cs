using BetterTeamsWebApp.Models.ViewModels;
using Models;
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
        public ActionResult EditMessage(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteMessage(int id)
        {
            MessageRepository messageRepo = new MessageRepository();
            messageRepo.Delete(id);
            return View("Messages","Admin");
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult EditUser(string email)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteUser(string email)
        {
            UserRepository userRepo = new UserRepository();
            userRepo.DeleteByEmail(email);
            return View("Users","Admin");
        }
        public ActionResult Rooms()
        {
            return View();
        }
        public ActionResult EditRoom(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteRoom(int id)
        {
            RoomRepository roomRepo = new RoomRepository();
            roomRepo.Delete(id);
            return View("Rooms","Admin");
        }
        public ActionResult Posts()
        {
            return View();
        }
        public ActionResult EditPost(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            PostRepository postRepo = new PostRepository();
            postRepo.Delete(id);
            return View("Post", "Admin");
        }
    }
}