using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterTeamsWebApp.Models.ViewModels;
using Repository;
using Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace BetterTeamsWebApp.Controllers
{
   
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
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
        
        [Authorize]
        [HttpGet]
        public JsonResult GetRooms()
        {
            RoomRepository roomRepo = new RoomRepository();
            List<Room> rooms = roomRepo.GetAll();

            List<string> roomnames = new List<string>();
            foreach (var item in rooms)
            {
                roomnames.Add(item.Name);
            }
            return Json(roomnames, JsonRequestBehavior.AllowGet);
        }

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
            newMessage.Receiver = userRepo.GetAdmin().Username;
            newMessage.Text = ContactMessage.Text;
            newMessage.DateTime = DateTime.Now;
            newMessage.Deleted = false;
            messageRepo.Add(newMessage);

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        [HttpPost]
        public JsonResult GetMessages(string UserTo)
        {
            string Username1 = User.Identity.Name;
            string Username2 = UserTo;
            UserRepository Userdb = new UserRepository();
            User U1 = Userdb.GetByUsername(Username1);
            User U2 = Userdb.GetByUsername(Username2);

            List<MessageVM> messages = new List<MessageVM>();
            MessageVM msg = new MessageVM();

            

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

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
			UserRepository userrepo = new UserRepository();
			UserVM uservm = new UserVM();
			var user = userrepo.GetByUsername(User.Identity.Name);

            uservm.Username = user.Username;
            uservm.Password = user.Password;
            uservm.Surname = user.Surname;
            uservm.Name = user.Name;
            uservm.Email = user.Email;
            uservm.Active = user.Active;
            uservm.Role = user.Role;
            uservm.DateOfBirth = user.DateOfBirth;
			return View(uservm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult PostEdit(UserVM uservm)
        {
            UserRepository ur = new UserRepository();
            User user = new User
            {
                Username = uservm.Username,
                Password = EncryptPassword(uservm.Password),
                Name = uservm.Name,
                Surname = uservm.Surname,
                Role = uservm.Role,
                Active = uservm.Active,
                Email = uservm.Email,
                DateOfBirth = uservm.DateOfBirth
            };
            ur.Update(user);
            return RedirectToAction("MyProfile", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyProfile()
        {
			UserRepository userrepo = new UserRepository();
			UserVM userVM = new UserVM();
			var user = userrepo.GetByUsername(User.Identity.Name);

			userVM.Username = user.Username;
			userVM.Password = user.Password;
			userVM.Surname = user.Surname;
			userVM.Name = user.Name;
			userVM.Email = user.Email;
			userVM.DateOfBirth = user.DateOfBirth;
			userVM.Active = user.Active;
			userVM.Role = user.Role;

			return View(userVM);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AllRooms()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public JsonResult SendPost(string PostText, string Sender, string Room, string dateTime)
        {
            //done baby --backenda --TODO: Save postVM to db and send it back to me plz i need it!
            UserRepository userRepo = new UserRepository();
            User sender = userRepo.GetByUsername(Sender);
            Post post = new Post
            {
                PostText=PostText,
                DateTime=DateTime.Now,
                Room=Room,
                Sender=sender.Email,
                Deleted=false
            };
            PostRepository postRepo = new PostRepository();
            postRepo.Add(post);

            return Json(post, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetPostsOfRoom(string Room)
        {
            PostVM newpost;

            List<PostVM> PostsList = new List<PostVM>();
			PostRepository postrepo = new PostRepository();
            UserRepository userRepo = new UserRepository();
            User sender = new User();
			var posts = postrepo.GetByRoom(Room);
			foreach (var post in posts)
			{
                sender = userRepo.GetByEmail(post.Sender);
				newpost = new PostVM();
				newpost.Id = post.Id;
				newpost.PostText = post.PostText;
				newpost.Room = post.Room;
				newpost.Sender = sender.Username;
				newpost.DateTime = post.DateTime;
                newpost.Deleted = post.Deleted;
				PostsList.Add(newpost);			
			}

            return Json(PostsList, JsonRequestBehavior.AllowGet);
        }
		#region Encryption
		public string EncryptPassword(string Password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(Password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
		#endregion
	}
}