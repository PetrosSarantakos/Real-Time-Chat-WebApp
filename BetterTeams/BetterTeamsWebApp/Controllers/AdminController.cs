using BetterTeamsWebApp.Models.ViewModels;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BetterTeamsWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult EditMessage (int id)
        {
            MessageRepository messageRepo = new MessageRepository();
            Message msg = messageRepo.GetById(id);
            MessageVM messageVM = new MessageVM
            {
                Id = msg.Id,
                Sender = msg.Sender,
                Receiver = msg.Receiver,
                Text = msg.Text,
                DateTime = msg.DateTime.ToShortDateString(),
                Deleted = msg.Deleted
            };
            return View(messageVM);
        }
		[HttpPost]
		public ActionResult EditMessage (MessageVM messagevm)
		{
			MessageRepository messagerepo = new MessageRepository();

            Message msg = new Message
            {
                Id = messagevm.Id,
                Text = messagevm.Text,
                Sender = messagevm.Sender,
                Receiver = messagevm.Receiver,
				DateTime = Convert.ToDateTime(messagevm.DateTime),
				Deleted = messagevm.Deleted
			};
			messagerepo.Update(msg);
			return RedirectToAction("EditMessage","Admin");
		}
       
        public ActionResult DeleteMessage(int id)
        {
            MessageRepository messageRepo = new MessageRepository();
            messageRepo.Delete(id);
            return RedirectToAction("Messages", "Admin");
        }
       
        public ActionResult Users()
        {
            List<UserVM> users = new List<UserVM>();
            UserVM user = new UserVM();
            UserRepository userRepo = new UserRepository();
            var usrs = userRepo.GetAll().ToList();
            for (int i = 0; i < usrs.Count; i++)
            {
                user = new UserVM();
                user.Username = usrs[i].Username;
                user.Email = usrs[i].Email;
                user.Name = usrs[i].Name;
                user.Surname = usrs[i].Surname;
                user.Role = usrs[i].Role;
                user.DateOfBirth = usrs[i].DateOfBirth;
                user.Active = usrs[i].Active;
                users.Add(user);
            }
            return View(users);
        }
        public ActionResult EditUser(string email)
        {
            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetByEmail(email);
            UserVM userVM = new UserVM
            {
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Active = user.Active,
                Password = user.Password,
                Role = user.Role,
                DateOfBirth = user.DateOfBirth
            };
            return View(userVM);


        }
        [HttpPost]
        public ActionResult EditUser(UserVM uservm)
        {
            UserRepository ur = new UserRepository();
            User user = new User
            {
                Username = uservm.Username,
                Password = uservm.Password,
                Name = uservm.Name,
                Surname = uservm.Surname,
                Role = uservm.Role,
                Active = uservm.Active,
                Email = uservm.Email,
                DateOfBirth = uservm.DateOfBirth
            };
            ur.Edit(user);
            return RedirectToAction("Users", "Admin");
        }

        
        public ActionResult DeleteUser(string email)
        {
            UserRepository userRepo = new UserRepository();
            userRepo.DeleteByEmail(email);
            return RedirectToAction("Users", "Admin");
        }
        
        public ActionResult Rooms()
        {
            List<Room> rooms = new List<Room>();
            Room room = new Room();
            RoomRepository roomRepo = new RoomRepository();
            rooms = roomRepo.GetAll().ToList();
            return View(rooms);
        }
       
        //public ActionResult DeleteRoom(int id)
        //{
        //    RoomRepository roomRepo = new RoomRepository();
        //    roomRepo.Delete(id);
        //    return RedirectToAction("Rooms", "Admin");
        //}
        public ActionResult Posts()
        {
            List<PostVM> posts = new List<PostVM>();
            PostVM post = new PostVM();
            PostRepository postRepo = new PostRepository();
            var psts = postRepo.GetAll().ToList();
            for (int i = 0; i < psts.Count; i++)
            {
                post = new PostVM();
                post.Id = psts[i].Id;
                post.PostText = psts[i].PostText;
                post.Sender = psts[i].Sender;
                post.DateTime = psts[i].DateTime;
                post.Room = psts[i].Room;
                post.Deleted = psts[i].Deleted;
                posts.Add(post);
            }
            return View(posts);

        }
        public ActionResult EditPost(int id)
        {
            PostRepository postRepo = new PostRepository();
            Post post = postRepo.GetById(id);
            PostVM postVM = new PostVM
            {
                Id = post.Id,
                PostText = post.PostText,
                Sender = post.Sender,
                DateTime = post.DateTime,
                Room = post.Room,
                Deleted = post.Deleted,
            };
            return View(postVM);
        }
        [HttpPost]
        public ActionResult EditPost(PostVM postvm)
        {
            PostRepository ur = new PostRepository();
            Post post = new Post
            {
                Id = postvm.Id,
                PostText = postvm.PostText,
                Sender = postvm.Sender,
                DateTime = postvm.DateTime,
                Deleted = postvm.Deleted,
                Room = postvm.Room
            };
            ur.Update(post);
            return RedirectToAction("Posts", "Admin"); 
        }
        public ActionResult DeletePost(int id)
        {
            PostRepository postRepo = new PostRepository();
            postRepo.Delete(id);
            return RedirectToAction("Posts", "Admin");
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