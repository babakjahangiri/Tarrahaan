using System.Web;
using System.Web.Optimization;

namespace Tarrahaan
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/assets/js/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/assets/js/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/assets/js/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/assets/js/bootstrap").Include(
                    "~/lib/bootstrap/js/bootstrap.js",
                    "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/assets/js/bootstrap-rtl").Include(
                    "~/lib/bootstrap-rtl/js/bootstrap-ar.js",
                    "~/Scripts/respond.js"));

           bundles.Add(new ScriptBundle("~/assets/js/slider").Include(
                   "~/lib/slippry/slippry.js"));

            bundles.Add(new ScriptBundle("~/assets/js/listprofilterresponsive").Include(
                 "~/lib/customjs/listprofilterresponsive.js"));

            bundles.Add(new StyleBundle("~/assets/css").Include(
                     "~/lib/bootstrap/css/bootstrap.min.css",
                     "~/lib/styles/TarrahaanStyles/StyleSheet-en.min.css"
                     ));

            bundles.Add(new StyleBundle("~/assets/css-rtl").Include(
                     "~/lib/bootstrap-rtl/css/bootstrap.min.css",
                     "~/lib/styles/TarrahaanStyles/StyleSheet-fa.min.css"
                     ));

             bundles.Add(new StyleBundle("~/assets/css-font").Include(
               "~/lib/fontawsome/font-awesome.css",
               "~/lib/itanicon/itanicon.min.css"));

            bundles.Add(new StyleBundle("~/assets/admincss").Include(
                     "~/lib/bootstrap/css/bootstrap.min.css",
                     "~/lib/styles/AdminStyles/StyleSheet-en.min.css"
                     ));

            bundles.Add(new StyleBundle("~/assets/css/hint-ltr").Include(
                 "~/lib/hint/hint.ltr.css"));

            bundles.Add(new StyleBundle("~/assets/css/slider").Include(
                    "~/lib/slippry/slippry.css"));

            bundles.Add(new StyleBundle("~/assets/css/PgwSlideshow").Include(
                    "~/lib/PgwSlideshow/pgwslideshow.css"));

            bundles.Add(new ScriptBundle("~/assets/js/PgwSlideshow").Include(
                "~/lib/PgwSlideshow/pgwslideshow.js"));

            bundles.Add(new ScriptBundle("~/assets/js/Angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-touch.js",
                 "~/Scripts/angular-animate.js"
               ));


            bundles.Add(new ScriptBundle("~/assets/js/Angular-UI-Bootstrap").Include(
                 "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
                ));

        

            BundleTable.EnableOptimizations = true;
        }
    }
}
