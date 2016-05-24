using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synapsr.Extensions
{
    public static class Account
    {
        public static string GetUserAvatar(this System.Data.Entity.DbSet<Synapsr.Models.User> users, string username)
        {
            var usr = users.FirstOrDefault(u => u.UserName == username);
            return usr.avatar_uri.Length==0 ? "male.png" : usr.avatar_uri;
        }
    }
}