using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using Synapsr.Models.ViewModels;
using Synapsr.Logistics;
using Synapsr.Models;

namespace Synapsr.Controllers
{
    public class OrganizrController : Controller
    {
        private Tuple<User, Elevation, DatabaseStore> _usrcontext = AccountManager.GetCurrentUser();
        private string _filename = AppVars.TimetableFilename;

        [HttpGet]
        public string ShowDetails(int id,bool isodd=false)
        {
            var usr = _usrcontext.Item1;
            var grname = _usrcontext.Item3.Groups.FirstOrDefault(f => f.Id == usr.GroupId);
            using (TimeTable tb = new TimeTable(_filename,grname.Name))
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
                if (_usrcontext.Item2.ElevationName=="Teacher")
                {
                    html+="<br/>"+ "<div class=\"row\">" +
                    "<div class=\"col-md-6\">" +
                    "<a class=\"btn btn-default\" value=\"Delete\" href=\"" + 
                    Url.Action("Remove", "Organizr") + "?id=" + id + 
                    "&isodd="+isodd.ToString()+"\">Delete</button>" +
                    "</div>" +
                    "</div>";
                }
                return html;
            }
        }

        [HttpGet]
        public ActionResult ChangeGroup(string grname)
        {
            if (_usrcontext != null)
                if (_usrcontext.Item2.ElevationName == "Teacher")
                {
                    var grid = _usrcontext.Item3.Groups.FirstOrDefault(f => f.Name == grname).Id;
                    var x = _usrcontext.Item3.Users.FirstOrDefault(f => f.UserName == _usrcontext.Item1.UserName).GroupId = grid;
                    _usrcontext.Item3.SaveChanges();
                    return RedirectToAction("Index", "Organizr");
                }
                else
                    return new HttpStatusCodeResult(401);
            else
                return new HttpStatusCodeResult(401);
        }
        
        // GET: Organizr
        public ActionResult Index()
        {
            
            if (_usrcontext==null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            else
                if (_usrcontext.Item2.ElevationName=="Teacher")
                    ViewBag.test = new SelectList(_usrcontext.Item3.Groups, "Name", "Name");

            string filename = Server.MapPath("~/Content/Sett/timetable.json");
            var grname = _usrcontext.Item3.Groups.FirstOrDefault(f => f.Id == _usrcontext.Item1.GroupId);
            TimeTable tb = new TimeTable(filename,grname.Name);
            return View(tb);
        }
       
        [HttpGet]
        public ActionResult Remove(int id,bool isodd=false)
        {
            
            if (_usrcontext!=null)
            {
                if (_usrcontext.Item2.ElevationName!="Teacher")
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            var grname = _usrcontext.Item3.Groups.FirstOrDefault(f => f.Id == _usrcontext.Item1.GroupId);
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

        public ActionResult AddSubject()
        {
            if (_usrcontext.Item2!=null)
            {
                if (_usrcontext.Item2.ElevationName!="Teacher")
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            ViewBag.grname = new SelectList(_usrcontext.Item3.Groups, "Name", "Name");
            return View();
        }
    }
}