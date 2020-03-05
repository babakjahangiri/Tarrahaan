using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Tarrahaan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("fonts/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            //routes.IgnoreRoute("favicon.ico");

            routes.MapMvcAttributeRoutes();
            // AreaRegistration.RegisterAllAreas();

            //routes.MapRoute(
            //  name: "AccountRoute",                                    // Route name 
            //  url: "{lang}/Account/{action}",
            //  constraints: new { lang = @"([a-zA-Z]{2})" },   // en or en-US// URL with parameters
            //  defaults: new { lang = "en", controller = "Account" },  // Parameter defaults :default is Home
            //  namespaces: new[] { "Tarrahaan.Controllers" }
            //  );


            routes.MapRoute(
            name: "ShowProperty",
            url: "{lang}/show/{proId}-{seoKeyword}",
            constraints: new
            {
                lang = @"([a-zA-Z]{2})", // en   - en-US
                proId = @"\d{1,19}",
                // SeoKeyword= @"^[0-9a-zA-Z_]{1,100}$"
            },
            defaults: new
            {
                lang = "en",
                controller = "Property",
                action = "Show",
                proId = "0",
                seoKeyword = ""
            }, namespaces: new[] { "Tarrahaan.Controllers" }
        );

            routes.MapRoute(
              name: "ListProperties",
              url: "{lang}/list/{praction}/{category}/{type}",  // fa/list/rent/villa
              constraints: new { lang = @"([a-zA-Z]{2})" },   // en or en-US
              defaults: new
              {
                  lang = "en",
                  controller = "Property",
                  action = "ListProperties",
                  praction = "",
                  category = "",
                  type = "",
                  page = 1
              }, namespaces: new[] { "Tarrahaan.Controllers" }
              );


            routes.MapRoute(
           name: "MainRoute",                                    // Route name 
           url: "{lang}/{action}",
           constraints: new { lang = @"([a-zA-Z]{2})" },   // en or en-US// URL with parameters
           defaults: new { lang = "en", controller = "Main", action = "Home" }  // Parameter defaults :default is Home
           , namespaces: new[] { "Tarrahaan.Controllers" }
           );


            // routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "catch-all",                                    
            //    url: "{*catchall}",
            //    defaults: new { lang = "en", controller = "Error", action = "NotFound" }  
            //    );


            //    routes.MapRoute(
            //     name: "Errors",
            //     url: "Error/{action}/{code}",
            //     defaults: new { controller = "Error", action = "Other", code = UrlParameter.Optional }
            //);

        }
    }
}
