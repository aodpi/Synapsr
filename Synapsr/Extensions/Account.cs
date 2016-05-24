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
            return users.FirstOrDefault(u => u.UserName == username).avatar_uri;
        }
    }
}