using System.Web.Mvc;
using Glimpse.AspNet.Tab;

namespace Tarrahaan.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WebAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
             
            context.MapRoute(
               "Admin_redirect",
               "WebAdmin/",
               new {Controller="WebAdmin", action = "Index"}
              // namespaces: new[] { "Tarrahaan.Areas.Admin.Controllers.WebAdminController" }
           );
 
            //context.MapRoute(
            //    name: "Admin_default",
            //    url: "WebAdmin/{lang}/{controller}/{action}", // en or en-US// URL with parameters
            //    constraints: new { lang = @"(\w{2})|(\w{2}-\w{2})" },
            //    defaults: new { lang = "en", controller = "AdminMain", action = "Dashboard" }
            //    //namespaces: new string[] { "Tarrahaan.Controllers.MainController" }
            //);

        }
    }
}