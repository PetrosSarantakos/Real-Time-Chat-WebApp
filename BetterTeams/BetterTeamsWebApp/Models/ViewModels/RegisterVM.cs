using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BetterTeamsWebApp.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "First Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        public string Surname {get; set; }
        
        [Required(ErrorMessage = "Username Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirmation Required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Date of birth Required")]
        public string DateOfBirth { get; set; }

        public string Role { get; set; }
    }
}