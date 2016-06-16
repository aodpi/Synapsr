using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Slack.Webhooks;
using Synapsr.Models.ViewModels;
using System.Net;
using Synapsr.Models;
using System.Web.Security;

namespace Synapsr.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseStore db = new DatabaseStore();

        public ActionResult AddNews()
        {
            var usr = Logistics.AccountManager.GetCurrentUser();
            if (usr != null)
            {
                if (usr.Item2.ElevationName == "Teacher")
                {
                    return View();
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        public ActionResult AddNews(NewsViewModel mdl)
        {
            
            if (ModelState.IsValid)
            {
                News x = new News
                {
                    body = "<p align='justify'>" + mdl.body.Replace(System.Environment.NewLine, "</p><p>") + "</p>",
                    title = mdl.title,
                    date_published = DateTime.Now,
                    pic_url = "/Content/Images/Site/arr.png"
                };
                db.News.Add(x);
                db.SaveChanges();
                if (mdl.inform)
                {
                    SlackClient scl = new SlackClient(Logistics.AppVars.SlackWebHook);
                    scl.Post(new SlackMessage
                    {
                        Channel = "general",
                        Text = "Noutate:-> " + mdl.title
                    });
                }
                ViewBag.status = "success";
            }
            return View();
        }
        public ActionResult NewsD(int Id)
        {
            var rz = db.News.FirstOrDefault(f => f.Id == Id);
            return View(rz);
        }
        public void Test()
        {
            Logistics.GroupsManagement.AddGroupsForCurrentYear(3, "IA");
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StructCat()
        {
            return View();
        }
        public ActionResult Timeline()
        {
            return View();
        }

        [HttpPost]
        public string GetNews(bool shortnews=false)
        {
            string list_string = "<ul id='demo3'>",
                item_start = "<li class='news-item'>",
                body_string = string.Empty,
                result_string = string.Empty;
            var news = (from s in db.News
                        orderby s.date_published descending
                        select s).Take(5).ToList();
            if (shortnews)
                news.ForEach(f =>
                {
                    if (f.title.Length > 60)
                        f.title = f.title.Substring(0, 60) + "...";
                });
            result_string += list_string;
            foreach (var item in news)
            {
                body_string = $"<table><tr><td rowspan='2'><img class='img-circle' style='margin-right:10px' width='60' src='{item.pic_url}'></td><td>{item.title}<br/> <a href='" +
                    Url.Action("NewsD", "Home", new { Id = item.Id }) + "'>Mai mult...</a></td></tr>" +
                    $"<tr><td>{item.date_published.ToString("dd/MM/yy HH:mm")}</td><tr></table>";
                result_string += item_start + body_string + "</li>";
            }
            return result_string + "</ul>"; 
        }
       
    }
}