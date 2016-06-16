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
        public static Tuple<User, Elevation,DatabaseStore> GetCurrentUser()
        {
            var req = HttpContext.Current.Request;
            if (req.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                //clasa context
                var db = new DatabaseStore();

                //citirea informatiei din fisierul cookie
                HttpCookie authCookie = req.Cookies[FormsAuthentication.FormsCookieName];
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                //citirea informatiei despre utilizator din baza de date
                var currusr = db.Users.FirstOrDefault(f => f.UserName == authTicket.Name);

                //determinarea elevatiei utilizatorului citit
                var elev = db.Elevations.First(e => e.Id == currusr.ElevationId);

                //returnarea rezultatului
                return new Tuple<User, Elevation, DatabaseStore>(currusr, elev, db);
            }
            else
                //fisierul cookie nu este prezent
                //utilizator nelogat
                return null;
        }
    }
}