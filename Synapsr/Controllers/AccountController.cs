using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Synapsr.Models;
using Synapsr.Models.ViewModels;
using System.Web.Security;
using Synapsr.Extensions;

namespace Synapsr.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Specialitate = new SelectList(DBInstance.Current.Specialities, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel mdl)
        {
            if (ModelState.IsValid)
            {
                
                if (mdl.AvatarImage!=null)
                {
                    var usr = DBInstance.Current.Users.FirstOrDefault(f => f.UserName == mdl.UserName);
                    if (!System.IO.Directory.Exists(Server.MapPath("~/UserStore/" + mdl.UserName + "/")))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/UserStore/"+mdl.UserName+"/"));
                    }

                    mdl.AvatarImage.SaveAs(Server.MapPath("~/UserStore/" + mdl.UserName + "/" + mdl.AvatarImage.FileName));
                    if (usr == null)
                    {
                        DBInstance.Current.Users.Add(new User()
                        {
                            UserName = mdl.UserName,
                            Password = Security.Encryption.Sha1Encode(mdl.Password),
                            ElevationId = 2,
                            avatar_uri = "/" + mdl.UserName + "/" + mdl.AvatarImage.FileName,
                            IdSpecialitate = mdl.Specialitate,
                            FirstName = mdl.FirstName,
                            LastName = mdl.LastName,
                            Email = mdl.Email
                        });
                        DBInstance.Current.SaveChanges();
                    }
                }
            }
            else
            {
                ViewBag.Specialitate = new SelectList(DBInstance.Current.Specialities, "Id", "Name");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            if (Response.Cookies[FormsAuthentication.FormsCookieName]!=null)
            {
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home");
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
                    return RedirectToAction("Index", "Home", new { @Err = "true" });
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}