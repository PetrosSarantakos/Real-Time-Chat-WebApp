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
       
        [HttpPost]
        public ActionResult DeleteMessage(int id)
        {
            MessageRepository messageRepo = new MessageRepository();
            messageRepo.Delete(id);
            return View("Messages","Admin");
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

		[HttpPost]
        public ActionResult EditUser(UserVM uservm)
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
			return View(); //TODO: THE VIEW WE WANT TO RETURN
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
            List<Room> rooms = new List<Room>();
            Room room = new Room();
            RoomRepository roomRepo = new RoomRepository();
            rooms = roomRepo.GetAll().ToList();
            return View(rooms);
        }
        public ActionResult EditRoom(Room room)
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
			return View(); //TODO: THE VIEW WE WANT TO RETURN
        }
        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            PostRepository postRepo = new PostRepository();
            postRepo.Delete(id);
            return View("Post", "Admin");
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