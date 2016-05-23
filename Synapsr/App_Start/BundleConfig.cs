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
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js").Include("~/fonts"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-{version}.js"));

            //Styles
            bundles.Add(new StyleBundle("~/bundles/styles/local").Include("~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/bundles/styles/bootstrap").Include("~/Content/bootstrap.css"));
            BundleTable.EnableOptimizations = false;
        }
    }
}