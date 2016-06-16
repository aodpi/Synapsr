using System.Web;
using Newtonsoft.Json;

namespace Synapsr.Logistics
{
    public class AppVars
    {

        private class SlackHookJson
        {
            public string webhook { get; set; }
        }

        public static string SlackWebHook
        {
            get
            {
                var slkjson= System.IO.File.ReadAllText(_slkhookfilename);
                var obj = JsonConvert.DeserializeObject<SlackHookJson>(slkjson);
                return obj.webhook;
            }
        }
        private static string _slkhookfilename = HttpContext.Current.Server.MapPath("~/Content/Sett/slack.json");
        public static string TimetableFilename = HttpContext.Current.Server.MapPath("~/Content/Sett/timetable.json");
    }
}