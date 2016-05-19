using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Slack.Webhooks;
namespace Synapsr.Controllers
{
    public class HomeController : Controller
    {
        private static SlackClient cl = new SlackClient("https://hooks.slack.com/services/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");
        // GET: Home
        public ActionResult Index()
        {
            SlackMessage msg = new SlackMessage();
            msg.Text = "Anunt: Maine la ora 8:00 in blocul 3 se va desfasura expozitia Creatia Studenteilor UTM.";
            cl.PostToChannels(msg, new List<string> { "general", "random" });
            return View();
        }
    }
}