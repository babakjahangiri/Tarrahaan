using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarrahaan.CoreLib;

namespace Tarrahaan.Helpers
{
    public static class SeoUtils
    {
        public static MvcHtmlString MakeSeoFriendly(this HtmlHelper htmlHelper, string input)
        {
            var outputString = CoreLib.WebSite.FriendlyUrl.Make(input);
            return MvcHtmlString.Create(outputString);
        }
    }
}