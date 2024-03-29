﻿using System.Web;
using System.Web.Optimization;

namespace GRH_ENGAGEMENT
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-3.4.1.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/metro").Include(
                     "~/Scripts/metro/js/metro.min.js", "~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/Site.css", "~/Content/metro/css/metro-all.min.css"));
        }
    }
}
