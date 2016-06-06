using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Synapsr.Models;

namespace Synapsr.Logistics
{
    public class AccountManager
    { 
        public static Tuple<User, Elevation> GetCurrentUser()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                DatabaseStore db = new DatabaseStore();
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var currusr = db.Users.FirstOrDefault(u => u.UserName == authTicket.Name);
                var elev = db.Elevations.FirstOrDefault(e => e.Id == currusr.ElevationId);
                return new Tuple<User, Elevation>(currusr, elev);
            }
            else
                return null;
        }
    }
}