using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Slack.Webhooks;
using Synapsr.Models.ViewModels;
using Synapsr.Models;
using System.Web.Security;

namespace Synapsr.Controllers
{
    public class HomeController : Controller
    {
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

       
    }
}