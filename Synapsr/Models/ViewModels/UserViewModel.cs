﻿using System.Linq;
using Synapsr.Security;
using System.ComponentModel.DataAnnotations;

namespace Synapsr.Models.ViewModels
{
    public class UserViewModel
    {
        [Required,Display(Name ="Username")]
        public string UserName { get; set; }

        [Required,DataType(DataType.Password),Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name ="Remember on this PC")]
        public bool RememberMe { get; set; }

        public bool IsValid(string username,string password)
        {
            using (DatabaseStore db=new DatabaseStore())
            {
                //password = Encryption.Sha1Encode(password);
                var psdhash = Encryption.Sha1Encode(password);
                User usr = db.Users.FirstOrDefault(f => f.UserName == username && f.Password == psdhash);
                return usr == null ? false : true;
            }
        }
    }
}