using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Synapsr.Models.ViewModels
{
    public class RegisterViewModel
    {
        [StringLength(20),Required]
        public string UserName { get; set; }
        [DataType(DataType.Password),Required]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage = "Passwords are not the same")]
        public string RepeatPassword { get; set; }
        [StringLength(40)]
        public string FirstName { get; set; }
        [StringLength(40)]
        public string SecondName { get; set; }

        [DataType(DataType.EmailAddress),Required]
        public string Email { get; set; }

        public int Specialitate { get; set; }
        public HttpPostedFileBase AvatarImage { get; set; }
    }
}