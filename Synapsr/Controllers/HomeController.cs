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
        private static readonly DatabaseStore db = new DatabaseStore();
        private static readonly SlackClient cl = new SlackClient("https://hooks.slack.com/services/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");
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Timeline()
        {
            return View();
        }

       
    }
}