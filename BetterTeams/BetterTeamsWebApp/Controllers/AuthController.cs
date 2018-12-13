using BetterTeamsWebApp.Models.ViewModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;

namespace BetterTeamsWebApp.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Register()
        {
            var userToRegister = new RegisterVM();

            return View(userToRegister);
        }
        [HttpPost]
        public ActionResult Register(RegisterVM userToRegister)
        {
            User user = new User()
            {
                Active = true,
                Username=userToRegister.Username,
                Password = userToRegister.Password,
                Name =userToRegister.Name,
                Surname= userToRegister.Surname,
                Email= userToRegister.Email,
                Role= "User",
                DateOfBirth= userToRegister.DateOfBirth.ToString()
            };

            UserRepository reg = new UserRepository();
            reg.Add(user);
            return RedirectToAction("Login");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }


        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM userToLogin)
        {
            if(!ModelState.IsValid)
            {
                return View(userToLogin);
            }

            UserRepository userRepo = new UserRepository();
            User user;
            user=userRepo.GetByUsername(userToLogin.Username);

			string EncryptedPassword = EncryptPassword(userToLogin.Password);

			if (user != null && user.Password == EncryptedPassword) 
            {
                var userRole = user.Role;
                var ticket = new FormsAuthenticationTicket(version: 1,
                                   name: userToLogin.Username,
                                   issueDate: DateTime.Now,
                                   expiration: DateTime.Now.AddDays(30),
                                   isPersistent: userToLogin.RememberMe,
                                   userData: userRole);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                    encryptedTicket);

                HttpContext.Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(userToLogin);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

		public string EncryptPassword(string Password)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(Password);
			byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
			return Convert.ToBase64String(inArray);
		}
	}
}