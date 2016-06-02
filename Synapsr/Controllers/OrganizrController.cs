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
        public string ShowDetails(int id)
        {
            string filename = Server.MapPath("~/Content/Sett/timetable.json");
            using (TimeTable tb = new TimeTable(filename))
            {
                var currev = tb.Events[id];
                string html = "<div class=\"row\">" +
                    "<div class=\"col-md-4\">" +
                    "<ul class=\"list-group\">" +
                    "<li class=\"list-group-item\"><b>Obiect: </b>" + currev.long_name + " </li>" +
                    "<li class=\"list-group-item\"><b>Auditoriu: </b>"+currev.auditoriu+"</li>"+
                    "<li class=\"list-group-item\"><b>Profesor: </b>" + currev.profesor + " </li>" +
                    "<li class=\"list-group-item\"><b>Inceput: </b>" + currev.start_date.Substring(currev.start_date.IndexOf(' ')) + " </li>" +
                    "<li class=\"list-group-item\"><b>Sfarsit: </b>" + currev.end_date.Substring(currev.end_date.IndexOf(' ')) + " </li>" +
                    "</ul></div></div>";
                return html;
            }
        }
        // GET: Organizr
        public ActionResult Index()
        {
            string filename = Server.MapPath("~/Content/Sett/timetable.json");
            TimeTable tb = new TimeTable(filename);
            tb.AddEvent(new TimeTable.Event
            {
                profesor = "Lazu Victoria",
                start_date = "06/02/2016 14:05:00",
                end_date = "06/02/2016 15:30:00",
                obiect = "LE",
                zi = "Vineri",
                link = "#",
                long_name = "Limba Engleza",
                auditoriu="609"
            });
            return View(tb.Events);
        }
    }
}