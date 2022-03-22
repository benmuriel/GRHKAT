using System.Web;
using System.Web.Optimization;

namespace GRH_4WEB
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //           "~/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/qrcode").Include(
                             "~/Scripts/qrcode.min.js")); 

            bundles.Add(new ScriptBundle("~/bundles/metro").Include(
                     "~/Scripts/metro/js/metro.min.js", "~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css"));
        }
    }
}
