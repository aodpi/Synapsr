﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synapsr.Controllers
{
    public class SlackController : Controller
    {

        public class Rootobject
        { 
            public List<Filename> filnames { get; set; }
        }

        public class Filename
        {
            public string currname { get; set; }
            public string initname { get; set; }
        }

        private Slack.Webhooks.SlackClient scl = new Slack.Webhooks.SlackClient("https://hooks.slack.com/services/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");
        // GET: Slack
        public ActionResult Index()
        {
            var xx = Logistics.AccountManager.GetCurrentUser();
            switch (xx == null)
            {
                case true:
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                case false:
                    if (xx.Item2.ElevationName != "Teacher")
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                    }
                    break;
            }
            return View();
        }

        [HttpGet]
        public FileResult Download(string fileguid, string type)
        {
            string ext = System.IO.Path.GetExtension(fileguid);

            string filepath = "~/SlackStore/" + fileguid;
            var urf = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(System.IO.File.ReadAllText(Server.MapPath("~/Content/Sett/json.json")));
            var ffff=urf.filnames.FirstOrDefault(u => u.currname == fileguid).initname;
            return File(filepath, type, ffff);
        }

        [HttpPost]
        public ActionResult Post(string message, HttpPostedFileBase file, string groupid)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/SlackStore/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/SlackStore/"));
            }
            string filename = string.Empty;
            if (file != null)
            {
                var ext = System.IO.Path.GetExtension(file.FileName);
                filename = Guid.NewGuid() + ext;
                file.SaveAs(Server.MapPath("~/SlackStore/" + filename));
                SaveConventions(filename, file.FileName);
                scl.Post(new Slack.Webhooks.SlackMessage
                {
                    Text = message + ": " + ResolveServerUrl(VirtualPathUtility.ToAbsolute("~/Slack/Download/?fileguid=" + filename + "&type=" + file.ContentType), false),
                    Channel = groupid.ToLower()
                });
                ViewBag.status = "ok";
                return View("Index");
            }
            else
            {
                ViewBag.status = "err";
                return View("Index");
            }
        }

        public void SaveConventions(string currentname,string initialname)
        {
            var jsonstring = System.IO.File.ReadAllText(Server.MapPath("~/Content/Sett/json.json"));
            var filnms = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(jsonstring);
            filnms.filnames.Add(new Filename { currname = currentname, initname = initialname });
            var outp = Newtonsoft.Json.JsonConvert.SerializeObject(filnms, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(Server.MapPath("~/Content/Sett/json.json"), outp);
        }
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }
    }
}