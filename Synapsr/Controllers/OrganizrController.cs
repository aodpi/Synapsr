using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using Synapsr.Models.ViewModels;
using Synapsr.Logistics;

namespace Synapsr.Controllers
{
    public class OrganizrController : Controller
    {
        
        [HttpGet]
        public string ShowDetails(int id,bool isodd=false)
        {
            Synapsr.Models.DatabaseStore db = new Models.DatabaseStore();
            string filename = Server.MapPath("~/Content/Sett/timetable.json");
            var usr = AccountManager.GetCurrentUser();
            var grname = db.Groups.FirstOrDefault(f => f.Id == usr.Item1.GroupId);
            using (TimeTable tb = new TimeTable(filename,grname.Name))
            {
                var currev = isodd ? tb.EventsI[id] : tb.Events[id];
                string html = " <div class=\"well\">" +
                    "<div class=\"row\">" +
                    "<div class=\"col-md-4\">" +
                    "<br />" +
                    "<b>Subiect:</b> " + currev.obiect +
                    "<br />" +
                    "<b>Auditoriu:</b> " + currev.auditoriu +
                    "<br />" +
                    "<b>Denumire Lunga: </b>" + currev.long_name +
                    "</div>" +
                    "<div class=\"col-md-4\">" +
                    "<br />" +
                    "<b>Inceput: </b>" + currev.start_date.Split(' ')[1].Substring(0, 5) +
                    "<br />" +
                    "<b>Sfarsit: </b>" + currev.end_date.Split(' ')[1].Substring(0, 5) +
                    "</div>" +
                    "<div class=\"col-md-4\">" +
                    "<br />" +
                    "<b>Profesor:</b> " + currev.profesor +
                    "<br />" +
                    "<b>Zi: </b>" + currev.zi +
                    "</div>" +
                    "</div>";
                if (usr.Item2.ElevationName=="Teacher")
                {
                    html+="<br/>"+ "<div class=\"row\">" +
                    "<div class=\"col-md-6\">" +
                    "<a class=\"btn btn-default\" value=\"Delete\" href=\"" + Url.Action("Remove", "Organizr") + "?id=" + id + "&isodd="+isodd.ToString()+"\">Delete</button>" +
                    "</div>" +
                    "</div>";
                }
                return html;
            }
        }

        [HttpGet]
        public ActionResult ChangeGroup(string grname)
        {
            Synapsr.Models.DatabaseStore db = new Models.DatabaseStore();
            var xx = Synapsr.Logistics.AccountManager.GetCurrentUser();
            if (xx!=null)
            {
                if (xx.Item2.ElevationName=="Teacher")
                {
                    var grid = db.Groups.FirstOrDefault(f => f.Name == grname).Id;
                    var x = db.Users.FirstOrDefault(f => f.UserName == xx.Item1.UserName).GroupId = grid;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Organizr");
        }
        // GET: Organizr
        public ActionResult Index()
        {
            Synapsr.Models.DatabaseStore db = new Models.DatabaseStore();
            var usr = Synapsr.Logistics.AccountManager.GetCurrentUser();
            if (usr==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                if (usr.Item2.ElevationName=="Teacher")
                {
                    ViewBag.test = new SelectList(db.Groups, "Name", "Name");
                }
            }
            string filename = Server.MapPath("~/Content/Sett/timetable.json");
            var grname = db.Groups.FirstOrDefault(f => f.Id == usr.Item1.GroupId);
            TimeTable tb = new TimeTable(filename,grname.Name);
            return View(tb);
        }
        
        [HttpGet]
        public ActionResult Remove(int id,bool isodd=false)
        {
            Synapsr.Models.DatabaseStore db = new Models.DatabaseStore();
            var ff = Synapsr.Logistics.AccountManager.GetCurrentUser();
            if (ff!=null)
            {
                if (ff.Item2.ElevationName!="Teacher")
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            var grname = db.Groups.FirstOrDefault(f => f.Id == ff.Item1.GroupId);
            string filename = Server.MapPath("~/Content/Sett/timetable.json");
            using (TimeTable tt = new TimeTable(filename, grname.Name))
            {
                tt.DeleteEvent(id, isodd);
            }
            return RedirectToAction("Index", "Organizr");
        }

        [HttpPost]
        public ActionResult Add(TimeTable.Event ev)
        {

            string dt = "06/06/2016 ";
            ev.start_date = DateTime.Parse(dt + ev.start_date).ToString(TimeTable.timeformat);
            ev.end_date = DateTime.Parse(dt + ev.end_date).ToString(TimeTable.timeformat);
            string filename = Server.MapPath("~/Content/Sett/timetable.json");
            using (TimeTable tt=new TimeTable(filename,ev.grname))
            {
                if (ev.isodd)
                    tt.AddEvent(ev, true);
                else
                    tt.AddEvent(ev);
            }
            return RedirectToAction("Index", "Organizr");
        }
        public ActionResult AddSubject(TimeTable.Event ev)
        {
            Synapsr.Models.DatabaseStore db = new Models.DatabaseStore();
            var ff = Synapsr.Logistics.AccountManager.GetCurrentUser();
            if (ff!=null)
            {
                if (ff.Item2.ElevationName!="Teacher")
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            ViewBag.grname = new SelectList(db.Groups, "Name", "Name");
            return View();
        }
    }
}