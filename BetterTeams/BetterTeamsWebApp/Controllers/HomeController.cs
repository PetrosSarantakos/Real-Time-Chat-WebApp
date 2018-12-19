using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterTeamsWebApp.Models.ViewModels;
using Repository;
using Models;
using System.Threading.Tasks;

namespace BetterTeamsWebApp.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            return View();
        }
        // GET: Home
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }

        [Authorize]
        public JsonResult GetUserRooms()
        {
            UserRepository userRepo = new UserRepository();
            RoomRepository roomRepo = new RoomRepository();
            User user = userRepo.GetByUsername(User.Identity.Name);
            List<string> userRooms = roomRepo.GetNameRoomsByEmail(user.Email);

            return Json(userRooms, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public JsonResult GetUsers()
        {
            UserRepository userRepo = new UserRepository();
            //User user = userRepo.GetByUsername(User.Identity.Name);
            List<User> allUsers = new List<User>();
            allUsers = userRepo.GetAll().ToList();
            var removeUser = allUsers.SingleOrDefault(x => x.Username == User.Identity.Name);
            allUsers.Remove(removeUser);
            List<string> usernames = new List<string>();
            foreach (var item in allUsers)
            {
                usernames.Add(item.Username);
            }

            return Json(usernames, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// ///////////////////////////
        /// </summary>
        /// <returns></returns>
        //public JsonResult GetRooms(UserVM user)
        //{
        //    UserRepository userRepo = new UserRepository();
        //    User pageUser = userRepo.GetByUsername(User.Identity.Name);
        //    RoomRepository roomRepo = new RoomRepository();
        //    List<Room> rooms = new List<Room>();
        //    var roomNames = roomRepo.GetAllRoomsByEmail(user.Email).ToList();
        //    foreach(var item in roomNames)
        //    {

        //    }
        //    return roomNames;

        //}
        //public JsonResult GetUsers()
        //{
        //    UserRepository userRepo = new UserRepository();
        //    RoomRepository roomRepo = new RoomRepository();
        //    List<User> users = new List<User>();
        //    var usersInRoom = roomRepo.GetAllUsersEmailInRoom()
        //}
       
           


        [HttpPost]
        public JsonResult ContactUs(ContactMessageVM ContactMessage)
        {
            MessageRepository messageRepo = new MessageRepository();
            UserRepository userRepo = new UserRepository();
            Message newMessage = new Message();
            if(userRepo.GetByEmail(ContactMessage.SenderEmail) == null)
            {
                newMessage.Sender = "NotRegistered";
            }
            else
            {
                newMessage.Sender = userRepo.GetByEmail(ContactMessage.SenderEmail).Username;
            }
            newMessage.Receiver = "admin";
            newMessage.Text = ContactMessage.Text;
            newMessage.DateTime = DateTime.Now;
            newMessage.Deleted = false;
            messageRepo.Add(newMessage);

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        
        public JsonResult GetMessages(string UserTo)
        {

            string Username1 = User.Identity.Name;
            string Username2 = UserTo;
            UserRepository Userdb = new UserRepository();
            User U1 = Userdb.GetByUsername(Username1);
            User U2 = Userdb.GetByUsername(Username2);

            List<MessageVM> messages = new List<MessageVM>();
            MessageVM msg = new MessageVM();

            //using(var messageRepo = new MessageRepository())
            //{
            //    var msges = messageRepo.GetBySenderRecveiverUsername(U1, U2);
            //    for (int i = 0; i < msges.Count; i++)
            //    {
            //        msg.Id = msges[i].Id;
            //        msg.Text = msges[i].Text;
            //        msg.Sender = msges[i].Sender;
            //        msg.Receiver = msges[i].Receiver;
            //        msg.DateTime = msges[i].DateTime.ToString();
            //        msg.Deleted = msges[i].Deleted;
            //        messages.Add(msg);
            //    }
            //    return Json(msges, JsonRequestBehavior.AllowGet);
            //}
            MessageRepository messageRepo = new MessageRepository();

            var msges = messageRepo.GetBySenderRecveiverUsername(U1, U2);
            for (int i = 0; i < msges.Count; i++)
            {
                msg = new MessageVM();
                msg.Id = msges[i].Id;
                msg.Text = msges[i].Text;
                msg.Sender = msges[i].Sender;
                msg.Receiver = msges[i].Receiver;
                msg.DateTime = msges[i].DateTime.ToString();
                msg.Deleted = msges[i].Deleted;
                messages.Add(msg);
            }
            return Json(messages, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ContactUs(MessageVM messageVM)
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
    }
}