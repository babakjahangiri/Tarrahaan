using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Tarrahaan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            ViewEngines.Engines.Clear();

            var customRazorEngine = new RazorViewEngine
            {
                PartialViewLocationFormats = new string[]
                {
                    "~/Views/{1}/Partial/{0}.cshtml", //View/ControllerName/Partial/myview.cshtml
                    "~/Views/Shared/{0}.cshtml", //View/Shared/myview.cshtml
                    "~/Views/{1}/{0}.cshtml" //View/ControllerName/myview.cshtml
                }
            };

            ViewEngines.Engines.Add(customRazorEngine);

            MvcHandler.DisableMvcResponseHeader = true; //Remove X-AspNetMvc-Version Header

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //MappingConfig.RegisterMaps();
        }
    }
}
