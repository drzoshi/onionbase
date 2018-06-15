using System.Web;
using System.Web.Optimization;

namespace Exo_Base.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/css").Include(
                "~/Content/bootstrap-sb-admin-themes/vendor/bootstrap/css/bootstrap.min.css",
                "~/Content/bootstrap-sb-admin-themes/vendor/font-awesome/css/font-awesome.min.css",
                "~/Content/bootstrap-sb-admin-themes/vendor/datatables/dataTables.bootstrap4.css",
                "~/Content/bootstrap-sb-admin-themes/css/sb-admin.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/themes/js").Include(
                //"~/Content/bootstrap-sb-admin-themes/vendor/jquery/jquery.min.js",
                "~/Content/bootstrap-sb-admin-themes/vendor/bootstrap/js/bootstrap.bundle.min.js",
                "~/Content/bootstrap-sb-admin-themes/vendor/jquery-easing/jquery.easing.min.js",
                "~/Content/bootstrap-sb-admin-themes/vendor/chart.js/Chart.min.js",
                "~/Content/bootstrap-sb-admin-themes/vendor/datatables/jquery.dataTables.js",
                "~/Content/bootstrap-sb-admin-themes/vendor/datatables/dataTables.bootstrap4.js",
                "~/Content/bootstrap-sb-admin-themes/js/sb-admin.min.js",
                "~/Content/bootstrap-sb-admin-themes/js/sb-admin-datatables.min.js",
                "~/Content/bootstrap-sb-admin-themes/js/sb-admin-charts.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/themes-login/css").Include(
                "~/Content/bootstrap-sb-admin-themes/vendor/bootstrap/css/bootstrap.min.css",
                "~/Content/bootstrap-sb-admin-themes/vendor/font-awesome/css/font-awesome.min.css",
                "~/Content/bootstrap-sb-admin-themes/css/sb-admin.css"
                ));
            bundles.Add(new ScriptBundle("~/Content/themes-login/js").Include(
                //"~/Content/bootstrap-sb-admin-themes/vendor/jquery/jquery.min.js",
                "~/Content/bootstrap-sb-admin-themes/vendor/bootstrap/js/bootstrap.bundle.min.js",
                "~/Content/bootstrap-sb-admin-themes/vendor/jquery-easing/jquery.easing.min.js"
                ));

            //To enable bundling and minification, set the debug value to "false". You can override the Web.config setting with the EnableOptimizations property on the BundleTable class
            //BundleTable.EnableOptimizations = true;
        }
    }
}
