using BetterTeamsWebApp.Models.ViewModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;

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
                Role= userToRegister.Role,
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
    }
}