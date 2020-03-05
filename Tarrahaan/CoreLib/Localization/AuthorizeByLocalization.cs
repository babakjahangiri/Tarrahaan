using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tarrahaan.CoreLib.Localization
{ 
    public class AuthorizeByLocalization:AuthorizeAttribute
    {
      
        protected override void HandleUnauthorizedRequest(AuthorizationContext ctx)
        {
            var langId = "en";
           var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            if (routeData != null)
            {
                langId = ctx.RouteData.Values["lang"].ToString();
            }

             var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
             var localizedRedirectUrl= new RedirectToRouteResult("loginRoute", new RouteValueDictionary(new {controller="Account", action = "Login", lang = @langId}));
             ctx.Result = localizedRedirectUrl;

            // base.HandleUnauthorizedRequest(ctx);
        }
    }
}