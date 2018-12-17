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
                msg.Message = msges[i].Text;
                msg.Sender = msges[i].Sender;
                msg.Receiver = msges[i].Receiver;
                msg.DateTime = msges[i].DateTime;
                msg.Deleted = msges[i].Deleted;
;               messages.Add(msg);
            }
            return View(messages);
        }
        public JsonResult GetUserRooms ()
        {
            UserRepository userRepo = new UserRepository();
            RoomRepository roomRepo = new RoomRepository();
            User user = userRepo.GetByUsername(User.Identity.Name);
            List<string> userRooms = roomRepo.GetAllRoomsByEmail(user.Email);

            return Json(userRooms, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetUsersInroom()
        //{
        //    UserRepository userRepo = new UserRepository();
        //    RoomRepository roomRepo = new RoomRepository();
        //    User user = userRepo.GetByUsername(User.Identity.Name);
        //    List<string> usersInRoom = new List<string>();
        //    usersInRoom = roomRepo.GetAllUsernamesInARoom()
        //    List<string> userNames = new List<string>();
        //    foreach(var item in users)
            
        //    return Json(userNames, JsonRequestBehavior.AllowGet);

        //}
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
            MessageVM msg = new MessageVM();

            MessageRepository messageRepo = new MessageRepository();

            var msges = messageRepo.GetBySenderRecveiverUsername(U1, U2);
            for (int i = 0; i < msges.Count; i++)
            {
                msg.Id = msges[i].Id;
                msg.Message = msges[i].Text;
                msg.Sender = msges[i].Sender;
                msg.Receiver = msges[i].Receiver;
                msg.DateTime = msges[i].DateTime;
                msg.Deleted = msges[i].Deleted;
                messages.Add(msg);
            }
            return Json(msges, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ContactUs(MessageVM messageVM)
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
    }
}