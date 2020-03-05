using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Tarrahaan.CoreLib.Localization
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        private readonly string _defaultLanguage;

        public LocalizationAttribute(string defaultLanguage = "en")
        {
            _defaultLanguage = defaultLanguage;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var lang = (string) filterContext.RouteData.Values["lang"] ?? _defaultLanguage;

            if (lang == "") return;
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang); // de-DE
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            }


            catch (Exception)
            {
                //  throw new NotSupportedException($"ERROR: Invalid language code '{lang}'.");
                //throw new Exception(ex.Message);
                throw new HttpException(404, "HTTP/1.1 404 Not Found");
            }
 
        }
    }
}