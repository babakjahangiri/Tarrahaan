using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarrahaan.CoreLib.WebSite;

namespace Tarrahaan.Helpers
{
    public static class cssActive
    {
        public static MvcHtmlString SetActiveClass(this HtmlHelper htmlHelper, string currentSectionName , string activeForSection)
        {
            var cssClass = "";
            if (string.Equals(currentSectionName, activeForSection, StringComparison.CurrentCultureIgnoreCase))
                cssClass = "active";
            return MvcHtmlString.Create(cssClass);
        }
    }
}