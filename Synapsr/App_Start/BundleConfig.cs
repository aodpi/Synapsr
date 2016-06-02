using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Synapsr
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts
            //Jquery, meh.
            bundles.Add(new ScriptBundle("~/bundles/jquery").
                Include("~/Scripts/jquery-{version}.js").
                Include("~/fonts"));

            //Lovely bootstrap.
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").
                Include("~/Scripts/bootstrap.js"));

            //Have no fucking idea what is this.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-{version}.js"));

            //Timetable
            bundles.Add(new ScriptBundle("~/bundles/timetable").
                Include("~/Scripts/timetable.min.js"));

            //Unobtrusive Validation.
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").
                Include("~/Scripts/jquery.validate.js").
                Include("~/Scripts/jquery.validate.unobtrusive.js"));

            //Styles
            bundles.Add(new StyleBundle("~/bundles/styles/local").
                Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/bundles/styles/bootstrap").
                Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/bundles/styles/timetable").
                Include("~/Content/timetablejs.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}