using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Synapsr.Models.ViewModels
{
    public class UserNameAttribute:ValidationAttribute
    {
        private readonly Synapsr.Models.DatabaseStore db = new DatabaseStore();
        public UserNameAttribute()
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                string val = value.ToString();
                var vrobj= db.Users.FirstOrDefault(u => u.UserName == val);
                if (vrobj!=null)
                {
                    return new ValidationResult("Username already exists");
                }
            }
            return ValidationResult.Success;
        }
    }
    public class RegisterViewModel
    {
        [StringLength(20),Required,UserName,Display(Name ="Username:"),DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.Password),Required,Display(Name ="Password:")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Passwords are not the same"),DataType(DataType.Password),Display(Name ="Repeat password:")]
        public string RepeatPassword { get; set; }

        [StringLength(40),Required]
        public string FirstName { get; set; }

        [StringLength(40),Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress),Required]
        public string Email { get; set; }

        public int Specialitate { get; set; }

        public HttpPostedFileBase AvatarImage { get; set; }

        public string Sex { get; set; }
    }
}