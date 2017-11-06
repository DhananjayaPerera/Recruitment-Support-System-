using System.Web;
using System.Web.Optimization;

namespace ARMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/BoostrapMinCSS").Include(
                        "~/Content/bootstrap.min.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapMin").Include(
                     "~/Scripts/bootstrap.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/JqueryMin").Include(
                "~/Scripts/jquery-{version}.js"                
            ));

            bundles.Add(new StyleBundle("~/Content/AdminSiteSkinsCSS").Include(
                        "~/Content/AdminSite/css/AdminLTE.min.css",
                        "~/Content/AdminSite/css/skins/_all-skins.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/AdminSite/plugins/icheck").Include(
                     "~/Content/AdminSite/plugins/iCheck/flat/blue.css",
                     "~/Content/AdminSite/plugins/select2/select2.min.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
                    "~/Content/AdminSite/plugins/iCheck/icheck.js",
                    "~/Content/AdminSite/plugins/select2/select2.full.min.js"
            ));




            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
