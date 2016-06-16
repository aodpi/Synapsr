using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Synapsr.Models;
using Synapsr.Models.ViewModels;
using System.Web.Security;
using Synapsr.Extensions;
using System.IO;
namespace Synapsr.Controllers
{
    public class AccountController : Controller
    {
        private DatabaseStore db = new DatabaseStore();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        private class Sex
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
        public ActionResult Register()
        {
            ViewBag.Specialitate = new SelectList(db.Specialities, "Id", "Name");
            List<Sex> ls = new List<Sex>() { new Sex { Name = "Male", Value = "Male" }, new Sex { Name = "Female", Value = "Female" } };
            ViewBag.Sex = new SelectList(ls, "Value", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel mdl)
        {
            if (ModelState.IsValid)
            {
                var usr = db.Users.FirstOrDefault(f => f.UserName == mdl.UserName);
                var x = db.RegCodes.FirstOrDefault(u => u.code == mdl.RegCode);
                if (mdl.AvatarImage!=null)
                {
                    if (!Directory.Exists(Server.MapPath("~/UserStore/" + mdl.UserName + "/")))
                        Directory.CreateDirectory(Server.MapPath("~/UserStore/"+mdl.UserName+"/"));

                    mdl.AvatarImage.SaveAs(Server.MapPath("~/UserStore/" + mdl.UserName + "/" + mdl.AvatarImage.FileName));
                    if (usr == null)
                    {
                        
                        db.Users.Add(new User()
                        {
                            UserName = mdl.UserName,
                            Password = Security.Encryption.Sha1Encode(mdl.Password),
                            ElevationId = x.code=="alpha_omegaqwe"?3:2,
                            avatar_uri = "/" + mdl.UserName + "/" + mdl.AvatarImage.FileName,
                            IdSpecialitate = mdl.Specialitate,
                            FirstName = mdl.FirstName,
                            LastName = mdl.LastName,
                            Email = mdl.Email,
                            Sex = mdl.Sex,
                            GroupId = x.GroupId
                        });
                        db.SaveChanges();
                        FormsAuthentication.SetAuthCookie(mdl.UserName, true);
                    }
                }
                else
                {
                    db.Users.Add(new User()
                    {
                        UserName = mdl.UserName,
                        Password = Security.Encryption.Sha1Encode(mdl.Password),
                        ElevationId = x.code=="alpha_omegaqwe"?3:2,
                        avatar_uri = mdl.Sex == "Male" ? "/male.png" : "/female.png",
                        IdSpecialitate = mdl.Specialitate,
                        FirstName = mdl.FirstName,
                        LastName = mdl.LastName,
                        Email = mdl.Email,
                        Sex = mdl.Sex,
                        GroupId = x.GroupId
                    });
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(mdl.UserName, true);
                }
            }
            else
            {
                ViewBag.Specialitate = new SelectList(db.Specialities, "Id", "Name");
                List<Sex> ls = new List<Sex>() { new Sex { Name = "Male", Value = "Male" }, new Sex { Name = "Female", Value = "Female" } };
                ViewBag.Sex = new SelectList(ls, "Value", "Name");
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
                if (mdl.IsValid(mdl.UserName, mdl.Password))
                    if (Request.Cookies[FormsAuthentication.FormsCookieName] == null)
                        FormsAuthentication.SetAuthCookie(mdl.UserName, mdl.RememberMe);
                    else
                        return RedirectToAction("Index", "Home", new { @Err = "invusr" });
                else
                    return RedirectToAction("Index", "Home", new { @Err = "nodata" });
            return RedirectToAction("Index", "Home");
        }
    }
}