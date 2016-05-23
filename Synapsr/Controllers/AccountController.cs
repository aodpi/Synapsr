using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Synapsr.Models;
using Synapsr.Models.ViewModels;
using System.Web.Security;

namespace Synapsr.Controllers
{
    public class AccountController : Controller
    {
        private static readonly DatabaseStore db = new DatabaseStore();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserViewModel mdl)
        {
            var usr = db.Users.FirstOrDefault(f => f.UserName == mdl.UserName);
            if (usr == null)
            {
                db.Users.Add(new User() { UserName = mdl.UserName, Password = Security.Encryption.Sha1Encode(mdl.Password), ElevationId = 1 });
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "User already exists");
                return View("Register", mdl);
            }
            if (ModelState.IsValid)
            {
                if (mdl.IsValid(mdl.UserName, mdl.Password))
                {

                    if (Request.Cookies[FormsAuthentication.FormsCookieName] == null)
                    {
                        FormsAuthentication.SetAuthCookie(mdl.UserName, mdl.RememberMe);
                    }
                }
                else
                {
                    ModelState.AddModelError("Register", "Login data incorrect");
                    return View("Register", mdl);
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Login(UserViewModel mdl)
        {
            if (ModelState.IsValid)
            {
                if (mdl.IsValid(mdl.UserName, mdl.Password))
                {

                    if (Request.Cookies[FormsAuthentication.FormsCookieName] == null)
                    {
                        FormsAuthentication.SetAuthCookie(mdl.UserName, mdl.RememberMe);
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "No such username");
                    return View("Register", mdl);
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}